using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ultima
{
    public static class Skills
    {
        public static int DefaultLength = 55;

        public static FileIndex FileIndex { get; } = new FileIndex("Skills.idx", "Skills.mul", DefaultLength, -1);

        public static Skill[] List { get; } = new Skill[DefaultLength];

        public static Skill GetSkill(int index)
        {
            if (List[index] != null)
            {
                return List[index];
            }

            var stream = FileIndex.Seek(index, out _, out _, out _);

            if (stream == null)
            {
                return List[index] = new Skill(SkillData.DefaultData);
            }

            return List[index] = LoadSkill(index, stream);
        }

        private static Skill LoadSkill(int index, Stream stream)
        {
            var bin = new BinaryReader(stream);

            var nameLength = FileIndex.Index[index].Length - 2;
            var extra = FileIndex.Index[index].Extra;

            var set1 = new byte[1];
            var set2 = new byte[nameLength];
            var set3 = new byte[1];

            bin.Read(set1, 0, 1);
            bin.Read(set2, 0, nameLength);
            bin.Read(set3, 0, 1);

            var useBtn = ToBool(set1);
            var name = ToString(set2);

            return new Skill(new SkillData(index, name, useBtn, extra, set3[0], null));
        }

        public static string ToString(byte[] data)
        {
            var sb = new StringBuilder(data.Length);

            foreach (var datum in data)
            {
                sb.Append(ToString(datum));
            }

            return sb.ToString();
        }

        public static bool ToBool(byte[] data)
        {
            return BitConverter.ToBoolean(data, 0);
        }

        public static string ToString(byte b)
        {
            return ToString((char) b);
        }

        public static string ToString(char c)
        {
            return c.ToString();
        }
    }

    public class Skill
    {
        public SkillData Data { get; private set; }

        public int Index { get; private set; } = -1;

        public bool UseButton { get; set; }

        public string Name { get; set; } = string.Empty;

        public SkillCategory Category { get; set; }

        public byte Unknown { get; private set; }

        public int ID => Index + 1;

        public Skill(SkillData data)
        {
            Data = data;

            Index = Data.Index;
            UseButton = Data.UseButton;
            Name = Data.Name;
            Category = Data.Category;
            Unknown = Data.Unknown;
        }

        public void ResetFromData()
        {
            Index = Data.Index;
            UseButton = Data.UseButton;
            Name = Data.Name;
            Category = Data.Category;
            Unknown = Data.Unknown;
        }

        public void ResetFromData(SkillData data)
        {
            Data = data;
            Index = Data.Index;
            UseButton = Data.UseButton;
            Name = Data.Name;
            Category = Data.Category;
            Unknown = Data.Unknown;
        }

        public override string ToString()
        {
            return $"{Index} ({Index:X4}) {(UseButton ? "[x]" : "[ ]")} {Name}";
        }
    }

    public sealed class SkillData
    {
        public static SkillData DefaultData => new SkillData(-1, "null", false, 0, 0x0, null);

        public int Index { get; } = -1;

        public string Name { get; } = string.Empty;

        public int Extra { get; }

        public bool UseButton { get; }

        public byte Unknown { get; }

        public SkillCategory Category { get; }

        public int NameLength => Name.Length;

        public SkillData(int index, string name, bool useButton, int extra, byte unk, SkillCategory mCategory)
        {
            Index = index;
            Category = mCategory;
            Name = name;
            UseButton = useButton;
            Extra = extra;
            Unknown = unk;
        }
    }

    public sealed class SkillCategories
    {
        public static SkillCategory[] List { get; private set; } = new SkillCategory[0];

        private SkillCategories()
        {
        }

        public static SkillCategory GetCategory(int index)
        {
            if (List.Length > 0)
            {
                if (index < List.Length)
                {
                    return List[index];
                }
            }

            List = LoadCategories();

            if (List.Length > 0)
            {
                return GetCategory(index);
            }

            return new SkillCategory(SkillCategoryData.DefaultData);
        }

        private static SkillCategory[] LoadCategories()
        {
            var list = new SkillCategory[0];

            var grpPath = Client.GetFilePath("skillgrp.mul");

            if (grpPath == null)
            {
                return new SkillCategory[0];
            }

            var toAdd = new List<SkillCategory>();

            using (var stream = new FileStream(grpPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var bin = new BinaryReader(stream);

                var START = new byte[4]; //File Start Offset

                bin.Read(START, 0, 4);

                var index = 0;

                long
                    x = stream.Length,
                    y = 0;

                while (y < x) //Position < Length
                {
                    var name = ParseName(stream);
                    var fileIndex = stream.Position - name.Length;

                    if (name.Length > 0)
                    {
                        toAdd.Add(new SkillCategory(new SkillCategoryData(fileIndex, index, name)));

                        y = stream.Position;

                        ++index;
                    }
                }
            }

            if (toAdd.Count > 0)
            {
                list = new SkillCategory[toAdd.Count];

                for (var i = 0; i < toAdd.Count; i++)
                {
                    list[i] = toAdd[i];
                }

                toAdd.Clear();
            }

            return list;
        }

        private static string ParseName(Stream stream)
        {
            var bin = new BinaryReader(stream);

            var tempName = string.Empty;

            var esc = false;

            while (!esc && bin.PeekChar() != -1)
            {
                var DATA = new byte[1];

                bin.Read(DATA, 0, 1);

                var c = (char) DATA[0];

                if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || char.IsPunctuation(c))
                {
                    tempName += Skills.ToString(c);
                    continue;
                }

                esc = true;
            }

            return tempName.Trim();
        }
    }

    public class SkillCategory
    {
        public SkillCategoryData Data { get; private set; }

        public int Index { get; private set; } = -1;

        public string Name { get; private set; } = string.Empty;

        public SkillCategory(SkillCategoryData data)
        {
            Data = data;
            Index = Data.Index;
            Name = Data.Name;
        }

        public void ResetFromData()
        {
            Index = Data.Index;
            Name = Data.Name;
        }

        public void ResetFromData(SkillCategoryData data)
        {
            Data = data;
            Index = Data.Index;
            Name = Data.Name;
        }
    }

    public sealed class SkillCategoryData
    {
        public static SkillCategoryData DefaultData => new SkillCategoryData(0, -1, "null");

        public long FileIndex { get; } = -1;

        public int Index { get; } = -1;

        public string Name { get; } = string.Empty;

        public SkillCategoryData(long fileIndex, int index, string name)
        {
            FileIndex = fileIndex;
            Index = index;
            Name = name;
        }
    }
}
// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: pb_spirit.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace PbSpirit {

  /// <summary>Holder for reflection information generated from pb_spirit.proto</summary>
  public static partial class PbSpiritReflection {

    #region Descriptor
    /// <summary>File descriptor for pb_spirit.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static PbSpiritReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg9wYl9zcGlyaXQucHJvdG8SCXBiX3NwaXJpdCKzAQoGU3Bpcml0EgoKAmlk",
            "GAEgASgJEgwKBG5hbWUYAiABKAkSLQoKc3BlY2lhbGlzdBgDIAEoDjIZLnBi",
            "X3NwaXJpdC5TcGVjaWFsaXN0VHlwZRIlCgZyYXJpdHkYBCABKA4yFS5wYl9z",
            "cGlyaXQuUmFyaXR5VHlwZRINCgVsZXZlbBgFIAEoBRIYChBza2lsbERlc2Ny",
            "aXB0aW9uGAogASgJEhAKCHNlbGVjdGVkGBQgASgIIjwKC1NwaXJpdFNraWxs",
            "EgoKAmlkGAEgASgFEgwKBG5hbWUYAyABKAkSEwoLZGVzY3JpcHRpb24YCiAB",
            "KAkqgQEKDlNwZWNpYWxpc3RUeXBlEgsKB1NUX05vbmUQABILCgdTVF9GaXJl",
            "EAESCgoGU1RfSWNlEAISCwoHU1RfSG9seRADEgsKB1NUX0V2aWwQBBIOCgpT",
            "VF9OYXR1cmFsEAUSDgoKU1RfTXlzdGVyeRAGEg8KC1NUX01hZ2ljaWFuEGQq",
            "RwoKUmFyaXR5VHlwZRILCgdSVF9Ob25lEAASDQoJUlRfQ29tbW9uEAESCwoH",
            "UlRfUmFyZRACEhAKDFJUX0xlZ2VuZGFyeRADQg5aDHBiL3BiX3NwaXJpdGIG",
            "cHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::PbSpirit.SpecialistType), typeof(global::PbSpirit.RarityType), }, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::PbSpirit.Spirit), global::PbSpirit.Spirit.Parser, new[]{ "Id", "Name", "Specialist", "Rarity", "Level", "SkillDescription", "Selected" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::PbSpirit.SpiritSkill), global::PbSpirit.SpiritSkill.Parser, new[]{ "Id", "Name", "Description" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Enums
  public enum SpecialistType {
    [pbr::OriginalName("ST_None")] StNone = 0,
    [pbr::OriginalName("ST_Fire")] StFire = 1,
    [pbr::OriginalName("ST_Ice")] StIce = 2,
    [pbr::OriginalName("ST_Holy")] StHoly = 3,
    [pbr::OriginalName("ST_Evil")] StEvil = 4,
    [pbr::OriginalName("ST_Natural")] StNatural = 5,
    [pbr::OriginalName("ST_Mystery")] StMystery = 6,
    [pbr::OriginalName("ST_Magician")] StMagician = 100,
  }

  public enum RarityType {
    [pbr::OriginalName("RT_None")] RtNone = 0,
    [pbr::OriginalName("RT_Common")] RtCommon = 1,
    [pbr::OriginalName("RT_Rare")] RtRare = 2,
    [pbr::OriginalName("RT_Legendary")] RtLegendary = 3,
  }

  #endregion

  #region Messages
  public sealed partial class Spirit : pb::IMessage<Spirit> {
    private static readonly pb::MessageParser<Spirit> _parser = new pb::MessageParser<Spirit>(() => new Spirit());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Spirit> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::PbSpirit.PbSpiritReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Spirit() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Spirit(Spirit other) : this() {
      id_ = other.id_;
      name_ = other.name_;
      specialist_ = other.specialist_;
      rarity_ = other.rarity_;
      level_ = other.level_;
      skillDescription_ = other.skillDescription_;
      selected_ = other.selected_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Spirit Clone() {
      return new Spirit(this);
    }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 1;
    private string id_ = "";
    /// <summary>
    ///??????ID
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Id {
      get { return id_; }
      set {
        id_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 2;
    private string name_ = "";
    /// <summary>
    ///??????
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "specialist" field.</summary>
    public const int SpecialistFieldNumber = 3;
    private global::PbSpirit.SpecialistType specialist_ = 0;
    /// <summary>
    ///??????????????????????????????????????????????????????????????????
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::PbSpirit.SpecialistType Specialist {
      get { return specialist_; }
      set {
        specialist_ = value;
      }
    }

    /// <summary>Field number for the "rarity" field.</summary>
    public const int RarityFieldNumber = 4;
    private global::PbSpirit.RarityType rarity_ = 0;
    /// <summary>
    ///?????????
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::PbSpirit.RarityType Rarity {
      get { return rarity_; }
      set {
        rarity_ = value;
      }
    }

    /// <summary>Field number for the "level" field.</summary>
    public const int LevelFieldNumber = 5;
    private int level_;
    /// <summary>
    ///??????
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Level {
      get { return level_; }
      set {
        level_ = value;
      }
    }

    /// <summary>Field number for the "skillDescription" field.</summary>
    public const int SkillDescriptionFieldNumber = 10;
    private string skillDescription_ = "";
    /// <summary>
    ///????????????
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string SkillDescription {
      get { return skillDescription_; }
      set {
        skillDescription_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "selected" field.</summary>
    public const int SelectedFieldNumber = 20;
    private bool selected_;
    /// <summary>
    ///????????????
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Selected {
      get { return selected_; }
      set {
        selected_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Spirit);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Spirit other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (Name != other.Name) return false;
      if (Specialist != other.Specialist) return false;
      if (Rarity != other.Rarity) return false;
      if (Level != other.Level) return false;
      if (SkillDescription != other.SkillDescription) return false;
      if (Selected != other.Selected) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Id.Length != 0) hash ^= Id.GetHashCode();
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (Specialist != 0) hash ^= Specialist.GetHashCode();
      if (Rarity != 0) hash ^= Rarity.GetHashCode();
      if (Level != 0) hash ^= Level.GetHashCode();
      if (SkillDescription.Length != 0) hash ^= SkillDescription.GetHashCode();
      if (Selected != false) hash ^= Selected.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Id.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Id);
      }
      if (Name.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Name);
      }
      if (Specialist != 0) {
        output.WriteRawTag(24);
        output.WriteEnum((int) Specialist);
      }
      if (Rarity != 0) {
        output.WriteRawTag(32);
        output.WriteEnum((int) Rarity);
      }
      if (Level != 0) {
        output.WriteRawTag(40);
        output.WriteInt32(Level);
      }
      if (SkillDescription.Length != 0) {
        output.WriteRawTag(82);
        output.WriteString(SkillDescription);
      }
      if (Selected != false) {
        output.WriteRawTag(160, 1);
        output.WriteBool(Selected);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Id.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Id);
      }
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (Specialist != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Specialist);
      }
      if (Rarity != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Rarity);
      }
      if (Level != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Level);
      }
      if (SkillDescription.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(SkillDescription);
      }
      if (Selected != false) {
        size += 2 + 1;
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Spirit other) {
      if (other == null) {
        return;
      }
      if (other.Id.Length != 0) {
        Id = other.Id;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.Specialist != 0) {
        Specialist = other.Specialist;
      }
      if (other.Rarity != 0) {
        Rarity = other.Rarity;
      }
      if (other.Level != 0) {
        Level = other.Level;
      }
      if (other.SkillDescription.Length != 0) {
        SkillDescription = other.SkillDescription;
      }
      if (other.Selected != false) {
        Selected = other.Selected;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            Id = input.ReadString();
            break;
          }
          case 18: {
            Name = input.ReadString();
            break;
          }
          case 24: {
            specialist_ = (global::PbSpirit.SpecialistType) input.ReadEnum();
            break;
          }
          case 32: {
            rarity_ = (global::PbSpirit.RarityType) input.ReadEnum();
            break;
          }
          case 40: {
            Level = input.ReadInt32();
            break;
          }
          case 82: {
            SkillDescription = input.ReadString();
            break;
          }
          case 160: {
            Selected = input.ReadBool();
            break;
          }
        }
      }
    }

  }

  public sealed partial class SpiritSkill : pb::IMessage<SpiritSkill> {
    private static readonly pb::MessageParser<SpiritSkill> _parser = new pb::MessageParser<SpiritSkill>(() => new SpiritSkill());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<SpiritSkill> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::PbSpirit.PbSpiritReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SpiritSkill() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SpiritSkill(SpiritSkill other) : this() {
      id_ = other.id_;
      name_ = other.name_;
      description_ = other.description_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SpiritSkill Clone() {
      return new SpiritSkill(this);
    }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 1;
    private int id_;
    /// <summary>
    ///??????ID
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 3;
    private string name_ = "";
    /// <summary>
    ///????????????
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "description" field.</summary>
    public const int DescriptionFieldNumber = 10;
    private string description_ = "";
    /// <summary>
    ///????????????
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Description {
      get { return description_; }
      set {
        description_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as SpiritSkill);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(SpiritSkill other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (Name != other.Name) return false;
      if (Description != other.Description) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Id != 0) hash ^= Id.GetHashCode();
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (Description.Length != 0) hash ^= Description.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Id != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Id);
      }
      if (Name.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Name);
      }
      if (Description.Length != 0) {
        output.WriteRawTag(82);
        output.WriteString(Description);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Id != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
      }
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (Description.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Description);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(SpiritSkill other) {
      if (other == null) {
        return;
      }
      if (other.Id != 0) {
        Id = other.Id;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.Description.Length != 0) {
        Description = other.Description;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            Id = input.ReadInt32();
            break;
          }
          case 26: {
            Name = input.ReadString();
            break;
          }
          case 82: {
            Description = input.ReadString();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code

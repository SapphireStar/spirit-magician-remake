// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: base.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Base {

  /// <summary>Holder for reflection information generated from base.proto</summary>
  public static partial class BaseReflection {

    #region Descriptor
    /// <summary>File descriptor for base.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static BaseReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CgpiYXNlLnByb3RvEgRiYXNlIt4BCgxVc2VyQmFzZUluZm8SCwoDdWlkGAEg",
            "ASgFEgwKBG5hbWUYAiABKAkSCgoCbHYYAyABKAUSDwoHYWNjb3VudBgEIAEo",
            "CRIOCgZmYWNlSUQYBSABKAUSDwoHZnJhbWVJRBgGIAEoBRIOCgZkaWNlSUQY",
            "CiABKAUSDQoFZ3JhZGUYFCABKAUSDQoFc2NvcmUYHiABKAUSDwoHdGl0bGVJ",
            "RBgoIAEoBRIRCglndWlsZE5hbWUYMiABKAkSEgoKZ3VpbGRCYWRnZRgzIAEo",
            "BRIPCgdndWlsZElEGDQgASgFQglaB3BiL2Jhc2ViBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Base.UserBaseInfo), global::Base.UserBaseInfo.Parser, new[]{ "Uid", "Name", "Lv", "Account", "FaceID", "FrameID", "DiceID", "Grade", "Score", "TitleID", "GuildName", "GuildBadge", "GuildID" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class UserBaseInfo : pb::IMessage<UserBaseInfo> {
    private static readonly pb::MessageParser<UserBaseInfo> _parser = new pb::MessageParser<UserBaseInfo>(() => new UserBaseInfo());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<UserBaseInfo> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Base.BaseReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UserBaseInfo() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UserBaseInfo(UserBaseInfo other) : this() {
      uid_ = other.uid_;
      name_ = other.name_;
      lv_ = other.lv_;
      account_ = other.account_;
      faceID_ = other.faceID_;
      frameID_ = other.frameID_;
      diceID_ = other.diceID_;
      grade_ = other.grade_;
      score_ = other.score_;
      titleID_ = other.titleID_;
      guildName_ = other.guildName_;
      guildBadge_ = other.guildBadge_;
      guildID_ = other.guildID_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UserBaseInfo Clone() {
      return new UserBaseInfo(this);
    }

    /// <summary>Field number for the "uid" field.</summary>
    public const int UidFieldNumber = 1;
    private int uid_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Uid {
      get { return uid_; }
      set {
        uid_ = value;
      }
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 2;
    private string name_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "lv" field.</summary>
    public const int LvFieldNumber = 3;
    private int lv_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Lv {
      get { return lv_; }
      set {
        lv_ = value;
      }
    }

    /// <summary>Field number for the "account" field.</summary>
    public const int AccountFieldNumber = 4;
    private string account_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Account {
      get { return account_; }
      set {
        account_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "faceID" field.</summary>
    public const int FaceIDFieldNumber = 5;
    private int faceID_;
    /// <summary>
    /// 脸型
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int FaceID {
      get { return faceID_; }
      set {
        faceID_ = value;
      }
    }

    /// <summary>Field number for the "frameID" field.</summary>
    public const int FrameIDFieldNumber = 6;
    private int frameID_;
    /// <summary>
    /// 头像框
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int FrameID {
      get { return frameID_; }
      set {
        frameID_ = value;
      }
    }

    /// <summary>Field number for the "diceID" field.</summary>
    public const int DiceIDFieldNumber = 10;
    private int diceID_;
    /// <summary>
    /// 骰子外形
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int DiceID {
      get { return diceID_; }
      set {
        diceID_ = value;
      }
    }

    /// <summary>Field number for the "grade" field.</summary>
    public const int GradeFieldNumber = 20;
    private int grade_;
    /// <summary>
    /// 段位
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Grade {
      get { return grade_; }
      set {
        grade_ = value;
      }
    }

    /// <summary>Field number for the "score" field.</summary>
    public const int ScoreFieldNumber = 30;
    private int score_;
    /// <summary>
    ///积分
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Score {
      get { return score_; }
      set {
        score_ = value;
      }
    }

    /// <summary>Field number for the "titleID" field.</summary>
    public const int TitleIDFieldNumber = 40;
    private int titleID_;
    /// <summary>
    /// 称号勋章
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int TitleID {
      get { return titleID_; }
      set {
        titleID_ = value;
      }
    }

    /// <summary>Field number for the "guildName" field.</summary>
    public const int GuildNameFieldNumber = 50;
    private string guildName_ = "";
    /// <summary>
    /// 公会名称
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string GuildName {
      get { return guildName_; }
      set {
        guildName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "guildBadge" field.</summary>
    public const int GuildBadgeFieldNumber = 51;
    private int guildBadge_;
    /// <summary>
    ///公会徽章
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int GuildBadge {
      get { return guildBadge_; }
      set {
        guildBadge_ = value;
      }
    }

    /// <summary>Field number for the "guildID" field.</summary>
    public const int GuildIDFieldNumber = 52;
    private int guildID_;
    /// <summary>
    ///公会ID
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int GuildID {
      get { return guildID_; }
      set {
        guildID_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as UserBaseInfo);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(UserBaseInfo other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Uid != other.Uid) return false;
      if (Name != other.Name) return false;
      if (Lv != other.Lv) return false;
      if (Account != other.Account) return false;
      if (FaceID != other.FaceID) return false;
      if (FrameID != other.FrameID) return false;
      if (DiceID != other.DiceID) return false;
      if (Grade != other.Grade) return false;
      if (Score != other.Score) return false;
      if (TitleID != other.TitleID) return false;
      if (GuildName != other.GuildName) return false;
      if (GuildBadge != other.GuildBadge) return false;
      if (GuildID != other.GuildID) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Uid != 0) hash ^= Uid.GetHashCode();
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (Lv != 0) hash ^= Lv.GetHashCode();
      if (Account.Length != 0) hash ^= Account.GetHashCode();
      if (FaceID != 0) hash ^= FaceID.GetHashCode();
      if (FrameID != 0) hash ^= FrameID.GetHashCode();
      if (DiceID != 0) hash ^= DiceID.GetHashCode();
      if (Grade != 0) hash ^= Grade.GetHashCode();
      if (Score != 0) hash ^= Score.GetHashCode();
      if (TitleID != 0) hash ^= TitleID.GetHashCode();
      if (GuildName.Length != 0) hash ^= GuildName.GetHashCode();
      if (GuildBadge != 0) hash ^= GuildBadge.GetHashCode();
      if (GuildID != 0) hash ^= GuildID.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Uid != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Uid);
      }
      if (Name.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Name);
      }
      if (Lv != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(Lv);
      }
      if (Account.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(Account);
      }
      if (FaceID != 0) {
        output.WriteRawTag(40);
        output.WriteInt32(FaceID);
      }
      if (FrameID != 0) {
        output.WriteRawTag(48);
        output.WriteInt32(FrameID);
      }
      if (DiceID != 0) {
        output.WriteRawTag(80);
        output.WriteInt32(DiceID);
      }
      if (Grade != 0) {
        output.WriteRawTag(160, 1);
        output.WriteInt32(Grade);
      }
      if (Score != 0) {
        output.WriteRawTag(240, 1);
        output.WriteInt32(Score);
      }
      if (TitleID != 0) {
        output.WriteRawTag(192, 2);
        output.WriteInt32(TitleID);
      }
      if (GuildName.Length != 0) {
        output.WriteRawTag(146, 3);
        output.WriteString(GuildName);
      }
      if (GuildBadge != 0) {
        output.WriteRawTag(152, 3);
        output.WriteInt32(GuildBadge);
      }
      if (GuildID != 0) {
        output.WriteRawTag(160, 3);
        output.WriteInt32(GuildID);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Uid != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Uid);
      }
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (Lv != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Lv);
      }
      if (Account.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Account);
      }
      if (FaceID != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(FaceID);
      }
      if (FrameID != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(FrameID);
      }
      if (DiceID != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(DiceID);
      }
      if (Grade != 0) {
        size += 2 + pb::CodedOutputStream.ComputeInt32Size(Grade);
      }
      if (Score != 0) {
        size += 2 + pb::CodedOutputStream.ComputeInt32Size(Score);
      }
      if (TitleID != 0) {
        size += 2 + pb::CodedOutputStream.ComputeInt32Size(TitleID);
      }
      if (GuildName.Length != 0) {
        size += 2 + pb::CodedOutputStream.ComputeStringSize(GuildName);
      }
      if (GuildBadge != 0) {
        size += 2 + pb::CodedOutputStream.ComputeInt32Size(GuildBadge);
      }
      if (GuildID != 0) {
        size += 2 + pb::CodedOutputStream.ComputeInt32Size(GuildID);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(UserBaseInfo other) {
      if (other == null) {
        return;
      }
      if (other.Uid != 0) {
        Uid = other.Uid;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.Lv != 0) {
        Lv = other.Lv;
      }
      if (other.Account.Length != 0) {
        Account = other.Account;
      }
      if (other.FaceID != 0) {
        FaceID = other.FaceID;
      }
      if (other.FrameID != 0) {
        FrameID = other.FrameID;
      }
      if (other.DiceID != 0) {
        DiceID = other.DiceID;
      }
      if (other.Grade != 0) {
        Grade = other.Grade;
      }
      if (other.Score != 0) {
        Score = other.Score;
      }
      if (other.TitleID != 0) {
        TitleID = other.TitleID;
      }
      if (other.GuildName.Length != 0) {
        GuildName = other.GuildName;
      }
      if (other.GuildBadge != 0) {
        GuildBadge = other.GuildBadge;
      }
      if (other.GuildID != 0) {
        GuildID = other.GuildID;
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
            Uid = input.ReadInt32();
            break;
          }
          case 18: {
            Name = input.ReadString();
            break;
          }
          case 24: {
            Lv = input.ReadInt32();
            break;
          }
          case 34: {
            Account = input.ReadString();
            break;
          }
          case 40: {
            FaceID = input.ReadInt32();
            break;
          }
          case 48: {
            FrameID = input.ReadInt32();
            break;
          }
          case 80: {
            DiceID = input.ReadInt32();
            break;
          }
          case 160: {
            Grade = input.ReadInt32();
            break;
          }
          case 240: {
            Score = input.ReadInt32();
            break;
          }
          case 320: {
            TitleID = input.ReadInt32();
            break;
          }
          case 402: {
            GuildName = input.ReadString();
            break;
          }
          case 408: {
            GuildBadge = input.ReadInt32();
            break;
          }
          case 416: {
            GuildID = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
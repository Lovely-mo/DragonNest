-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
local IDIPMESSAGE_PB = require("IdipMessage_pb")
local IDIPPUNISHDATA_PB = require("IdipPunishData_pb")
local PLATNOTICE_PB = require("PlatNotice_pb")
local IDIPHINTDATA_PB = require("IdipHintData_pb")
module('IdipData_pb')


IDIPDATA = protobuf.Descriptor();
local IDIPDATA_MESS_FIELD = protobuf.FieldDescriptor();
local IDIPDATA_PUNISHINFO_FIELD = protobuf.FieldDescriptor();
local IDIPDATA_LASTSENDANTIADDICTIONTIME_FIELD = protobuf.FieldDescriptor();
local IDIPDATA_ISSENDANTIADDICTIONREMIND_FIELD = protobuf.FieldDescriptor();
local IDIPDATA_PICURL_FIELD = protobuf.FieldDescriptor();
local IDIPDATA_NOTICE_FIELD = protobuf.FieldDescriptor();
local IDIPDATA_XINYUE_HINT_FIELD = protobuf.FieldDescriptor();
local IDIPDATA_HINTDATA_FIELD = protobuf.FieldDescriptor();
local IDIPDATA_ANTIADDICTIONREMINDCOUNT_FIELD = protobuf.FieldDescriptor();
local IDIPDATA_ADULTTYPE_FIELD = protobuf.FieldDescriptor();
local IDIPDATA_HGFLAG_FIELD = protobuf.FieldDescriptor();
local IDIPDATA_HGBANTIME_FIELD = protobuf.FieldDescriptor();
local IDIPDATA_HGGAMETIME_FIELD = protobuf.FieldDescriptor();
local IDIPDATA_ISGETHG_FIELD = protobuf.FieldDescriptor();

IDIPDATA_MESS_FIELD.name = "mess"
IDIPDATA_MESS_FIELD.full_name = ".KKSG.IdipData.mess"
IDIPDATA_MESS_FIELD.number = 1
IDIPDATA_MESS_FIELD.index = 0
IDIPDATA_MESS_FIELD.label = 1
IDIPDATA_MESS_FIELD.has_default_value = false
IDIPDATA_MESS_FIELD.default_value = nil
IDIPDATA_MESS_FIELD.message_type = IDIPMESSAGE_PB.IDIPMESSAGE
IDIPDATA_MESS_FIELD.type = 11
IDIPDATA_MESS_FIELD.cpp_type = 10

IDIPDATA_PUNISHINFO_FIELD.name = "punishInfo"
IDIPDATA_PUNISHINFO_FIELD.full_name = ".KKSG.IdipData.punishInfo"
IDIPDATA_PUNISHINFO_FIELD.number = 2
IDIPDATA_PUNISHINFO_FIELD.index = 1
IDIPDATA_PUNISHINFO_FIELD.label = 3
IDIPDATA_PUNISHINFO_FIELD.has_default_value = false
IDIPDATA_PUNISHINFO_FIELD.default_value = {}
IDIPDATA_PUNISHINFO_FIELD.message_type = IDIPPUNISHDATA_PB.IDIPPUNISHDATA
IDIPDATA_PUNISHINFO_FIELD.type = 11
IDIPDATA_PUNISHINFO_FIELD.cpp_type = 10

IDIPDATA_LASTSENDANTIADDICTIONTIME_FIELD.name = "lastSendAntiAddictionTime"
IDIPDATA_LASTSENDANTIADDICTIONTIME_FIELD.full_name = ".KKSG.IdipData.lastSendAntiAddictionTime"
IDIPDATA_LASTSENDANTIADDICTIONTIME_FIELD.number = 3
IDIPDATA_LASTSENDANTIADDICTIONTIME_FIELD.index = 2
IDIPDATA_LASTSENDANTIADDICTIONTIME_FIELD.label = 1
IDIPDATA_LASTSENDANTIADDICTIONTIME_FIELD.has_default_value = false
IDIPDATA_LASTSENDANTIADDICTIONTIME_FIELD.default_value = 0
IDIPDATA_LASTSENDANTIADDICTIONTIME_FIELD.type = 13
IDIPDATA_LASTSENDANTIADDICTIONTIME_FIELD.cpp_type = 3

IDIPDATA_ISSENDANTIADDICTIONREMIND_FIELD.name = "isSendAntiAddictionRemind"
IDIPDATA_ISSENDANTIADDICTIONREMIND_FIELD.full_name = ".KKSG.IdipData.isSendAntiAddictionRemind"
IDIPDATA_ISSENDANTIADDICTIONREMIND_FIELD.number = 4
IDIPDATA_ISSENDANTIADDICTIONREMIND_FIELD.index = 3
IDIPDATA_ISSENDANTIADDICTIONREMIND_FIELD.label = 1
IDIPDATA_ISSENDANTIADDICTIONREMIND_FIELD.has_default_value = false
IDIPDATA_ISSENDANTIADDICTIONREMIND_FIELD.default_value = false
IDIPDATA_ISSENDANTIADDICTIONREMIND_FIELD.type = 8
IDIPDATA_ISSENDANTIADDICTIONREMIND_FIELD.cpp_type = 7

IDIPDATA_PICURL_FIELD.name = "picUrl"
IDIPDATA_PICURL_FIELD.full_name = ".KKSG.IdipData.picUrl"
IDIPDATA_PICURL_FIELD.number = 5
IDIPDATA_PICURL_FIELD.index = 4
IDIPDATA_PICURL_FIELD.label = 1
IDIPDATA_PICURL_FIELD.has_default_value = false
IDIPDATA_PICURL_FIELD.default_value = ""
IDIPDATA_PICURL_FIELD.type = 9
IDIPDATA_PICURL_FIELD.cpp_type = 9

IDIPDATA_NOTICE_FIELD.name = "notice"
IDIPDATA_NOTICE_FIELD.full_name = ".KKSG.IdipData.notice"
IDIPDATA_NOTICE_FIELD.number = 6
IDIPDATA_NOTICE_FIELD.index = 5
IDIPDATA_NOTICE_FIELD.label = 3
IDIPDATA_NOTICE_FIELD.has_default_value = false
IDIPDATA_NOTICE_FIELD.default_value = {}
IDIPDATA_NOTICE_FIELD.message_type = PLATNOTICE_PB.PLATNOTICE
IDIPDATA_NOTICE_FIELD.type = 11
IDIPDATA_NOTICE_FIELD.cpp_type = 10

IDIPDATA_XINYUE_HINT_FIELD.name = "xinyue_hint"
IDIPDATA_XINYUE_HINT_FIELD.full_name = ".KKSG.IdipData.xinyue_hint"
IDIPDATA_XINYUE_HINT_FIELD.number = 7
IDIPDATA_XINYUE_HINT_FIELD.index = 6
IDIPDATA_XINYUE_HINT_FIELD.label = 1
IDIPDATA_XINYUE_HINT_FIELD.has_default_value = false
IDIPDATA_XINYUE_HINT_FIELD.default_value = false
IDIPDATA_XINYUE_HINT_FIELD.type = 8
IDIPDATA_XINYUE_HINT_FIELD.cpp_type = 7

IDIPDATA_HINTDATA_FIELD.name = "hintdata"
IDIPDATA_HINTDATA_FIELD.full_name = ".KKSG.IdipData.hintdata"
IDIPDATA_HINTDATA_FIELD.number = 8
IDIPDATA_HINTDATA_FIELD.index = 7
IDIPDATA_HINTDATA_FIELD.label = 3
IDIPDATA_HINTDATA_FIELD.has_default_value = false
IDIPDATA_HINTDATA_FIELD.default_value = {}
IDIPDATA_HINTDATA_FIELD.message_type = IDIPHINTDATA_PB.IDIPHINTDATA
IDIPDATA_HINTDATA_FIELD.type = 11
IDIPDATA_HINTDATA_FIELD.cpp_type = 10

IDIPDATA_ANTIADDICTIONREMINDCOUNT_FIELD.name = "AntiAddictionRemindCount"
IDIPDATA_ANTIADDICTIONREMINDCOUNT_FIELD.full_name = ".KKSG.IdipData.AntiAddictionRemindCount"
IDIPDATA_ANTIADDICTIONREMINDCOUNT_FIELD.number = 9
IDIPDATA_ANTIADDICTIONREMINDCOUNT_FIELD.index = 8
IDIPDATA_ANTIADDICTIONREMINDCOUNT_FIELD.label = 1
IDIPDATA_ANTIADDICTIONREMINDCOUNT_FIELD.has_default_value = false
IDIPDATA_ANTIADDICTIONREMINDCOUNT_FIELD.default_value = 0
IDIPDATA_ANTIADDICTIONREMINDCOUNT_FIELD.type = 13
IDIPDATA_ANTIADDICTIONREMINDCOUNT_FIELD.cpp_type = 3

IDIPDATA_ADULTTYPE_FIELD.name = "AdultType"
IDIPDATA_ADULTTYPE_FIELD.full_name = ".KKSG.IdipData.AdultType"
IDIPDATA_ADULTTYPE_FIELD.number = 10
IDIPDATA_ADULTTYPE_FIELD.index = 9
IDIPDATA_ADULTTYPE_FIELD.label = 1
IDIPDATA_ADULTTYPE_FIELD.has_default_value = false
IDIPDATA_ADULTTYPE_FIELD.default_value = 0
IDIPDATA_ADULTTYPE_FIELD.type = 5
IDIPDATA_ADULTTYPE_FIELD.cpp_type = 1

IDIPDATA_HGFLAG_FIELD.name = "hgFlag"
IDIPDATA_HGFLAG_FIELD.full_name = ".KKSG.IdipData.hgFlag"
IDIPDATA_HGFLAG_FIELD.number = 11
IDIPDATA_HGFLAG_FIELD.index = 10
IDIPDATA_HGFLAG_FIELD.label = 1
IDIPDATA_HGFLAG_FIELD.has_default_value = false
IDIPDATA_HGFLAG_FIELD.default_value = 0
IDIPDATA_HGFLAG_FIELD.type = 5
IDIPDATA_HGFLAG_FIELD.cpp_type = 1

IDIPDATA_HGBANTIME_FIELD.name = "hgBanTime"
IDIPDATA_HGBANTIME_FIELD.full_name = ".KKSG.IdipData.hgBanTime"
IDIPDATA_HGBANTIME_FIELD.number = 12
IDIPDATA_HGBANTIME_FIELD.index = 11
IDIPDATA_HGBANTIME_FIELD.label = 1
IDIPDATA_HGBANTIME_FIELD.has_default_value = false
IDIPDATA_HGBANTIME_FIELD.default_value = 0
IDIPDATA_HGBANTIME_FIELD.type = 13
IDIPDATA_HGBANTIME_FIELD.cpp_type = 3

IDIPDATA_HGGAMETIME_FIELD.name = "hgGameTime"
IDIPDATA_HGGAMETIME_FIELD.full_name = ".KKSG.IdipData.hgGameTime"
IDIPDATA_HGGAMETIME_FIELD.number = 13
IDIPDATA_HGGAMETIME_FIELD.index = 12
IDIPDATA_HGGAMETIME_FIELD.label = 1
IDIPDATA_HGGAMETIME_FIELD.has_default_value = false
IDIPDATA_HGGAMETIME_FIELD.default_value = 0
IDIPDATA_HGGAMETIME_FIELD.type = 13
IDIPDATA_HGGAMETIME_FIELD.cpp_type = 3

IDIPDATA_ISGETHG_FIELD.name = "isGetHg"
IDIPDATA_ISGETHG_FIELD.full_name = ".KKSG.IdipData.isGetHg"
IDIPDATA_ISGETHG_FIELD.number = 14
IDIPDATA_ISGETHG_FIELD.index = 13
IDIPDATA_ISGETHG_FIELD.label = 1
IDIPDATA_ISGETHG_FIELD.has_default_value = false
IDIPDATA_ISGETHG_FIELD.default_value = false
IDIPDATA_ISGETHG_FIELD.type = 8
IDIPDATA_ISGETHG_FIELD.cpp_type = 7

IDIPDATA.name = "IdipData"
IDIPDATA.full_name = ".KKSG.IdipData"
IDIPDATA.nested_types = {}
IDIPDATA.enum_types = {}
IDIPDATA.fields = {IDIPDATA_MESS_FIELD, IDIPDATA_PUNISHINFO_FIELD, IDIPDATA_LASTSENDANTIADDICTIONTIME_FIELD, IDIPDATA_ISSENDANTIADDICTIONREMIND_FIELD, IDIPDATA_PICURL_FIELD, IDIPDATA_NOTICE_FIELD, IDIPDATA_XINYUE_HINT_FIELD, IDIPDATA_HINTDATA_FIELD, IDIPDATA_ANTIADDICTIONREMINDCOUNT_FIELD, IDIPDATA_ADULTTYPE_FIELD, IDIPDATA_HGFLAG_FIELD, IDIPDATA_HGBANTIME_FIELD, IDIPDATA_HGGAMETIME_FIELD, IDIPDATA_ISGETHG_FIELD}
IDIPDATA.is_extendable = false
IDIPDATA.extensions = {}

IdipData = protobuf.Message(IDIPDATA)

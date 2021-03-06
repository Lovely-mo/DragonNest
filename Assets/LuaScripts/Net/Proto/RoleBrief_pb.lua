-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
local VEC3_PB = require("Vec3_pb")
local OUTLOOK_PB = require("OutLook_pb")
local ROLETYPE_PB = require("RoleType_pb")
module('RoleBrief_pb')


ROLEBRIEF = protobuf.Descriptor();
local ROLEBRIEF_TYPE_FIELD = protobuf.FieldDescriptor();
local ROLEBRIEF_NAME_FIELD = protobuf.FieldDescriptor();
local ROLEBRIEF_ROLEID_FIELD = protobuf.FieldDescriptor();
local ROLEBRIEF_ACCOUNTID_FIELD = protobuf.FieldDescriptor();
local ROLEBRIEF_LEVEL_FIELD = protobuf.FieldDescriptor();
local ROLEBRIEF_EXP_FIELD = protobuf.FieldDescriptor();
local ROLEBRIEF_MAXEXP_FIELD = protobuf.FieldDescriptor();
local ROLEBRIEF_POSITION_FIELD = protobuf.FieldDescriptor();
local ROLEBRIEF_SCENEID_FIELD = protobuf.FieldDescriptor();
local ROLEBRIEF_FACE_FIELD = protobuf.FieldDescriptor();
local ROLEBRIEF_OFFLINETIME_FIELD = protobuf.FieldDescriptor();
local ROLEBRIEF_COMPLETEGUIDESTAGE_FIELD = protobuf.FieldDescriptor();
local ROLEBRIEF_TUTORIALBITS_FIELD = protobuf.FieldDescriptor();
local ROLEBRIEF_ONLIMETIME_FIELD = protobuf.FieldDescriptor();
local ROLEBRIEF_AUCTIONPOINT_FIELD = protobuf.FieldDescriptor();
local ROLEBRIEF_CAMPID_FIELD = protobuf.FieldDescriptor();
local ROLEBRIEF_ACCOUNTNUMBERLASTDAY_FIELD = protobuf.FieldDescriptor();
local ROLEBRIEF_LASTACCOUNTTIME_FIELD = protobuf.FieldDescriptor();
local ROLEBRIEF_NICKID_FIELD = protobuf.FieldDescriptor();
local ROLEBRIEF_TUTORIALBITSARRAY_FIELD = protobuf.FieldDescriptor();
local ROLEBRIEF_TITLEID_FIELD = protobuf.FieldDescriptor();
local ROLEBRIEF_PAYMEMBERID_FIELD = protobuf.FieldDescriptor();
local ROLEBRIEF_CHANGENAMECOUNT_FIELD = protobuf.FieldDescriptor();
local ROLEBRIEF_OP_FIELD = protobuf.FieldDescriptor();
local ROLEBRIEF_INITTIME_FIELD = protobuf.FieldDescriptor();

ROLEBRIEF_TYPE_FIELD.name = "type"
ROLEBRIEF_TYPE_FIELD.full_name = ".KKSG.RoleBrief.type"
ROLEBRIEF_TYPE_FIELD.number = 1
ROLEBRIEF_TYPE_FIELD.index = 0
ROLEBRIEF_TYPE_FIELD.label = 1
ROLEBRIEF_TYPE_FIELD.has_default_value = false
ROLEBRIEF_TYPE_FIELD.default_value = nil
ROLEBRIEF_TYPE_FIELD.enum_type = ROLETYPE_PB.ROLETYPE
ROLEBRIEF_TYPE_FIELD.type = 14
ROLEBRIEF_TYPE_FIELD.cpp_type = 8

ROLEBRIEF_NAME_FIELD.name = "name"
ROLEBRIEF_NAME_FIELD.full_name = ".KKSG.RoleBrief.name"
ROLEBRIEF_NAME_FIELD.number = 2
ROLEBRIEF_NAME_FIELD.index = 1
ROLEBRIEF_NAME_FIELD.label = 1
ROLEBRIEF_NAME_FIELD.has_default_value = false
ROLEBRIEF_NAME_FIELD.default_value = ""
ROLEBRIEF_NAME_FIELD.type = 9
ROLEBRIEF_NAME_FIELD.cpp_type = 9

ROLEBRIEF_ROLEID_FIELD.name = "roleID"
ROLEBRIEF_ROLEID_FIELD.full_name = ".KKSG.RoleBrief.roleID"
ROLEBRIEF_ROLEID_FIELD.number = 3
ROLEBRIEF_ROLEID_FIELD.index = 2
ROLEBRIEF_ROLEID_FIELD.label = 1
ROLEBRIEF_ROLEID_FIELD.has_default_value = false
ROLEBRIEF_ROLEID_FIELD.default_value = 0
ROLEBRIEF_ROLEID_FIELD.type = 4
ROLEBRIEF_ROLEID_FIELD.cpp_type = 4

ROLEBRIEF_ACCOUNTID_FIELD.name = "accountID"
ROLEBRIEF_ACCOUNTID_FIELD.full_name = ".KKSG.RoleBrief.accountID"
ROLEBRIEF_ACCOUNTID_FIELD.number = 4
ROLEBRIEF_ACCOUNTID_FIELD.index = 3
ROLEBRIEF_ACCOUNTID_FIELD.label = 1
ROLEBRIEF_ACCOUNTID_FIELD.has_default_value = false
ROLEBRIEF_ACCOUNTID_FIELD.default_value = ""
ROLEBRIEF_ACCOUNTID_FIELD.type = 9
ROLEBRIEF_ACCOUNTID_FIELD.cpp_type = 9

ROLEBRIEF_LEVEL_FIELD.name = "level"
ROLEBRIEF_LEVEL_FIELD.full_name = ".KKSG.RoleBrief.level"
ROLEBRIEF_LEVEL_FIELD.number = 5
ROLEBRIEF_LEVEL_FIELD.index = 4
ROLEBRIEF_LEVEL_FIELD.label = 1
ROLEBRIEF_LEVEL_FIELD.has_default_value = false
ROLEBRIEF_LEVEL_FIELD.default_value = 0
ROLEBRIEF_LEVEL_FIELD.type = 13
ROLEBRIEF_LEVEL_FIELD.cpp_type = 3

ROLEBRIEF_EXP_FIELD.name = "exp"
ROLEBRIEF_EXP_FIELD.full_name = ".KKSG.RoleBrief.exp"
ROLEBRIEF_EXP_FIELD.number = 6
ROLEBRIEF_EXP_FIELD.index = 5
ROLEBRIEF_EXP_FIELD.label = 1
ROLEBRIEF_EXP_FIELD.has_default_value = false
ROLEBRIEF_EXP_FIELD.default_value = 0
ROLEBRIEF_EXP_FIELD.type = 4
ROLEBRIEF_EXP_FIELD.cpp_type = 4

ROLEBRIEF_MAXEXP_FIELD.name = "maxexp"
ROLEBRIEF_MAXEXP_FIELD.full_name = ".KKSG.RoleBrief.maxexp"
ROLEBRIEF_MAXEXP_FIELD.number = 7
ROLEBRIEF_MAXEXP_FIELD.index = 6
ROLEBRIEF_MAXEXP_FIELD.label = 1
ROLEBRIEF_MAXEXP_FIELD.has_default_value = false
ROLEBRIEF_MAXEXP_FIELD.default_value = 0
ROLEBRIEF_MAXEXP_FIELD.type = 4
ROLEBRIEF_MAXEXP_FIELD.cpp_type = 4

ROLEBRIEF_POSITION_FIELD.name = "position"
ROLEBRIEF_POSITION_FIELD.full_name = ".KKSG.RoleBrief.position"
ROLEBRIEF_POSITION_FIELD.number = 8
ROLEBRIEF_POSITION_FIELD.index = 7
ROLEBRIEF_POSITION_FIELD.label = 1
ROLEBRIEF_POSITION_FIELD.has_default_value = false
ROLEBRIEF_POSITION_FIELD.default_value = nil
ROLEBRIEF_POSITION_FIELD.message_type = VEC3_PB.VEC3
ROLEBRIEF_POSITION_FIELD.type = 11
ROLEBRIEF_POSITION_FIELD.cpp_type = 10

ROLEBRIEF_SCENEID_FIELD.name = "sceneID"
ROLEBRIEF_SCENEID_FIELD.full_name = ".KKSG.RoleBrief.sceneID"
ROLEBRIEF_SCENEID_FIELD.number = 9
ROLEBRIEF_SCENEID_FIELD.index = 8
ROLEBRIEF_SCENEID_FIELD.label = 1
ROLEBRIEF_SCENEID_FIELD.has_default_value = false
ROLEBRIEF_SCENEID_FIELD.default_value = 0
ROLEBRIEF_SCENEID_FIELD.type = 5
ROLEBRIEF_SCENEID_FIELD.cpp_type = 1

ROLEBRIEF_FACE_FIELD.name = "face"
ROLEBRIEF_FACE_FIELD.full_name = ".KKSG.RoleBrief.face"
ROLEBRIEF_FACE_FIELD.number = 10
ROLEBRIEF_FACE_FIELD.index = 9
ROLEBRIEF_FACE_FIELD.label = 1
ROLEBRIEF_FACE_FIELD.has_default_value = false
ROLEBRIEF_FACE_FIELD.default_value = 0.0
ROLEBRIEF_FACE_FIELD.type = 2
ROLEBRIEF_FACE_FIELD.cpp_type = 6

ROLEBRIEF_OFFLINETIME_FIELD.name = "offlineTime"
ROLEBRIEF_OFFLINETIME_FIELD.full_name = ".KKSG.RoleBrief.offlineTime"
ROLEBRIEF_OFFLINETIME_FIELD.number = 11
ROLEBRIEF_OFFLINETIME_FIELD.index = 10
ROLEBRIEF_OFFLINETIME_FIELD.label = 1
ROLEBRIEF_OFFLINETIME_FIELD.has_default_value = false
ROLEBRIEF_OFFLINETIME_FIELD.default_value = 0
ROLEBRIEF_OFFLINETIME_FIELD.type = 13
ROLEBRIEF_OFFLINETIME_FIELD.cpp_type = 3

ROLEBRIEF_COMPLETEGUIDESTAGE_FIELD.name = "completeguidestage"
ROLEBRIEF_COMPLETEGUIDESTAGE_FIELD.full_name = ".KKSG.RoleBrief.completeguidestage"
ROLEBRIEF_COMPLETEGUIDESTAGE_FIELD.number = 12
ROLEBRIEF_COMPLETEGUIDESTAGE_FIELD.index = 11
ROLEBRIEF_COMPLETEGUIDESTAGE_FIELD.label = 1
ROLEBRIEF_COMPLETEGUIDESTAGE_FIELD.has_default_value = false
ROLEBRIEF_COMPLETEGUIDESTAGE_FIELD.default_value = false
ROLEBRIEF_COMPLETEGUIDESTAGE_FIELD.type = 8
ROLEBRIEF_COMPLETEGUIDESTAGE_FIELD.cpp_type = 7

ROLEBRIEF_TUTORIALBITS_FIELD.name = "tutorialBits"
ROLEBRIEF_TUTORIALBITS_FIELD.full_name = ".KKSG.RoleBrief.tutorialBits"
ROLEBRIEF_TUTORIALBITS_FIELD.number = 13
ROLEBRIEF_TUTORIALBITS_FIELD.index = 12
ROLEBRIEF_TUTORIALBITS_FIELD.label = 1
ROLEBRIEF_TUTORIALBITS_FIELD.has_default_value = false
ROLEBRIEF_TUTORIALBITS_FIELD.default_value = 0
ROLEBRIEF_TUTORIALBITS_FIELD.type = 4
ROLEBRIEF_TUTORIALBITS_FIELD.cpp_type = 4

ROLEBRIEF_ONLIMETIME_FIELD.name = "onlimetime"
ROLEBRIEF_ONLIMETIME_FIELD.full_name = ".KKSG.RoleBrief.onlimetime"
ROLEBRIEF_ONLIMETIME_FIELD.number = 14
ROLEBRIEF_ONLIMETIME_FIELD.index = 13
ROLEBRIEF_ONLIMETIME_FIELD.label = 1
ROLEBRIEF_ONLIMETIME_FIELD.has_default_value = false
ROLEBRIEF_ONLIMETIME_FIELD.default_value = 0
ROLEBRIEF_ONLIMETIME_FIELD.type = 13
ROLEBRIEF_ONLIMETIME_FIELD.cpp_type = 3

ROLEBRIEF_AUCTIONPOINT_FIELD.name = "auctionPoint"
ROLEBRIEF_AUCTIONPOINT_FIELD.full_name = ".KKSG.RoleBrief.auctionPoint"
ROLEBRIEF_AUCTIONPOINT_FIELD.number = 15
ROLEBRIEF_AUCTIONPOINT_FIELD.index = 14
ROLEBRIEF_AUCTIONPOINT_FIELD.label = 1
ROLEBRIEF_AUCTIONPOINT_FIELD.has_default_value = false
ROLEBRIEF_AUCTIONPOINT_FIELD.default_value = 0
ROLEBRIEF_AUCTIONPOINT_FIELD.type = 13
ROLEBRIEF_AUCTIONPOINT_FIELD.cpp_type = 3

ROLEBRIEF_CAMPID_FIELD.name = "campID"
ROLEBRIEF_CAMPID_FIELD.full_name = ".KKSG.RoleBrief.campID"
ROLEBRIEF_CAMPID_FIELD.number = 16
ROLEBRIEF_CAMPID_FIELD.index = 15
ROLEBRIEF_CAMPID_FIELD.label = 1
ROLEBRIEF_CAMPID_FIELD.has_default_value = false
ROLEBRIEF_CAMPID_FIELD.default_value = 0
ROLEBRIEF_CAMPID_FIELD.type = 13
ROLEBRIEF_CAMPID_FIELD.cpp_type = 3

ROLEBRIEF_ACCOUNTNUMBERLASTDAY_FIELD.name = "accountNumberLastDay"
ROLEBRIEF_ACCOUNTNUMBERLASTDAY_FIELD.full_name = ".KKSG.RoleBrief.accountNumberLastDay"
ROLEBRIEF_ACCOUNTNUMBERLASTDAY_FIELD.number = 17
ROLEBRIEF_ACCOUNTNUMBERLASTDAY_FIELD.index = 16
ROLEBRIEF_ACCOUNTNUMBERLASTDAY_FIELD.label = 1
ROLEBRIEF_ACCOUNTNUMBERLASTDAY_FIELD.has_default_value = false
ROLEBRIEF_ACCOUNTNUMBERLASTDAY_FIELD.default_value = 0
ROLEBRIEF_ACCOUNTNUMBERLASTDAY_FIELD.type = 13
ROLEBRIEF_ACCOUNTNUMBERLASTDAY_FIELD.cpp_type = 3

ROLEBRIEF_LASTACCOUNTTIME_FIELD.name = "lastAccountTime"
ROLEBRIEF_LASTACCOUNTTIME_FIELD.full_name = ".KKSG.RoleBrief.lastAccountTime"
ROLEBRIEF_LASTACCOUNTTIME_FIELD.number = 18
ROLEBRIEF_LASTACCOUNTTIME_FIELD.index = 17
ROLEBRIEF_LASTACCOUNTTIME_FIELD.label = 1
ROLEBRIEF_LASTACCOUNTTIME_FIELD.has_default_value = false
ROLEBRIEF_LASTACCOUNTTIME_FIELD.default_value = 0
ROLEBRIEF_LASTACCOUNTTIME_FIELD.type = 4
ROLEBRIEF_LASTACCOUNTTIME_FIELD.cpp_type = 4

ROLEBRIEF_NICKID_FIELD.name = "nickID"
ROLEBRIEF_NICKID_FIELD.full_name = ".KKSG.RoleBrief.nickID"
ROLEBRIEF_NICKID_FIELD.number = 19
ROLEBRIEF_NICKID_FIELD.index = 18
ROLEBRIEF_NICKID_FIELD.label = 1
ROLEBRIEF_NICKID_FIELD.has_default_value = false
ROLEBRIEF_NICKID_FIELD.default_value = 0
ROLEBRIEF_NICKID_FIELD.type = 13
ROLEBRIEF_NICKID_FIELD.cpp_type = 3

ROLEBRIEF_TUTORIALBITSARRAY_FIELD.name = "tutorialBitsArray"
ROLEBRIEF_TUTORIALBITSARRAY_FIELD.full_name = ".KKSG.RoleBrief.tutorialBitsArray"
ROLEBRIEF_TUTORIALBITSARRAY_FIELD.number = 20
ROLEBRIEF_TUTORIALBITSARRAY_FIELD.index = 19
ROLEBRIEF_TUTORIALBITSARRAY_FIELD.label = 1
ROLEBRIEF_TUTORIALBITSARRAY_FIELD.has_default_value = false
ROLEBRIEF_TUTORIALBITSARRAY_FIELD.default_value = ""
ROLEBRIEF_TUTORIALBITSARRAY_FIELD.type = 12
ROLEBRIEF_TUTORIALBITSARRAY_FIELD.cpp_type = 9

ROLEBRIEF_TITLEID_FIELD.name = "titleID"
ROLEBRIEF_TITLEID_FIELD.full_name = ".KKSG.RoleBrief.titleID"
ROLEBRIEF_TITLEID_FIELD.number = 21
ROLEBRIEF_TITLEID_FIELD.index = 20
ROLEBRIEF_TITLEID_FIELD.label = 1
ROLEBRIEF_TITLEID_FIELD.has_default_value = false
ROLEBRIEF_TITLEID_FIELD.default_value = 0
ROLEBRIEF_TITLEID_FIELD.type = 13
ROLEBRIEF_TITLEID_FIELD.cpp_type = 3

ROLEBRIEF_PAYMEMBERID_FIELD.name = "paymemberid"
ROLEBRIEF_PAYMEMBERID_FIELD.full_name = ".KKSG.RoleBrief.paymemberid"
ROLEBRIEF_PAYMEMBERID_FIELD.number = 22
ROLEBRIEF_PAYMEMBERID_FIELD.index = 21
ROLEBRIEF_PAYMEMBERID_FIELD.label = 1
ROLEBRIEF_PAYMEMBERID_FIELD.has_default_value = false
ROLEBRIEF_PAYMEMBERID_FIELD.default_value = 0
ROLEBRIEF_PAYMEMBERID_FIELD.type = 13
ROLEBRIEF_PAYMEMBERID_FIELD.cpp_type = 3

ROLEBRIEF_CHANGENAMECOUNT_FIELD.name = "changenamecount"
ROLEBRIEF_CHANGENAMECOUNT_FIELD.full_name = ".KKSG.RoleBrief.changenamecount"
ROLEBRIEF_CHANGENAMECOUNT_FIELD.number = 23
ROLEBRIEF_CHANGENAMECOUNT_FIELD.index = 22
ROLEBRIEF_CHANGENAMECOUNT_FIELD.label = 1
ROLEBRIEF_CHANGENAMECOUNT_FIELD.has_default_value = false
ROLEBRIEF_CHANGENAMECOUNT_FIELD.default_value = 0
ROLEBRIEF_CHANGENAMECOUNT_FIELD.type = 13
ROLEBRIEF_CHANGENAMECOUNT_FIELD.cpp_type = 3

ROLEBRIEF_OP_FIELD.name = "op"
ROLEBRIEF_OP_FIELD.full_name = ".KKSG.RoleBrief.op"
ROLEBRIEF_OP_FIELD.number = 24
ROLEBRIEF_OP_FIELD.index = 23
ROLEBRIEF_OP_FIELD.label = 1
ROLEBRIEF_OP_FIELD.has_default_value = false
ROLEBRIEF_OP_FIELD.default_value = nil
ROLEBRIEF_OP_FIELD.message_type = OUTLOOK_PB.OUTLOOKOP
ROLEBRIEF_OP_FIELD.type = 11
ROLEBRIEF_OP_FIELD.cpp_type = 10

ROLEBRIEF_INITTIME_FIELD.name = "inittime"
ROLEBRIEF_INITTIME_FIELD.full_name = ".KKSG.RoleBrief.inittime"
ROLEBRIEF_INITTIME_FIELD.number = 25
ROLEBRIEF_INITTIME_FIELD.index = 24
ROLEBRIEF_INITTIME_FIELD.label = 1
ROLEBRIEF_INITTIME_FIELD.has_default_value = false
ROLEBRIEF_INITTIME_FIELD.default_value = 0
ROLEBRIEF_INITTIME_FIELD.type = 4
ROLEBRIEF_INITTIME_FIELD.cpp_type = 4

ROLEBRIEF.name = "RoleBrief"
ROLEBRIEF.full_name = ".KKSG.RoleBrief"
ROLEBRIEF.nested_types = {}
ROLEBRIEF.enum_types = {}
ROLEBRIEF.fields = {ROLEBRIEF_TYPE_FIELD, ROLEBRIEF_NAME_FIELD, ROLEBRIEF_ROLEID_FIELD, ROLEBRIEF_ACCOUNTID_FIELD, ROLEBRIEF_LEVEL_FIELD, ROLEBRIEF_EXP_FIELD, ROLEBRIEF_MAXEXP_FIELD, ROLEBRIEF_POSITION_FIELD, ROLEBRIEF_SCENEID_FIELD, ROLEBRIEF_FACE_FIELD, ROLEBRIEF_OFFLINETIME_FIELD, ROLEBRIEF_COMPLETEGUIDESTAGE_FIELD, ROLEBRIEF_TUTORIALBITS_FIELD, ROLEBRIEF_ONLIMETIME_FIELD, ROLEBRIEF_AUCTIONPOINT_FIELD, ROLEBRIEF_CAMPID_FIELD, ROLEBRIEF_ACCOUNTNUMBERLASTDAY_FIELD, ROLEBRIEF_LASTACCOUNTTIME_FIELD, ROLEBRIEF_NICKID_FIELD, ROLEBRIEF_TUTORIALBITSARRAY_FIELD, ROLEBRIEF_TITLEID_FIELD, ROLEBRIEF_PAYMEMBERID_FIELD, ROLEBRIEF_CHANGENAMECOUNT_FIELD, ROLEBRIEF_OP_FIELD, ROLEBRIEF_INITTIME_FIELD}
ROLEBRIEF.is_extendable = false
ROLEBRIEF.extensions = {}

RoleBrief = protobuf.Message(ROLEBRIEF)


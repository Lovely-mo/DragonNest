-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
local PKBASEHIST_PB = require("PkBaseHist_pb")
local PKONEREC_PB = require("PkOneRec_pb")
module('PkRecordSubInfo_pb')


PKRECORDSUBINFO = protobuf.Descriptor();
local PKRECORDSUBINFO_POINT_FIELD = protobuf.FieldDescriptor();
local PKRECORDSUBINFO_REWARDCOUNT_FIELD = protobuf.FieldDescriptor();
local PKRECORDSUBINFO_SEASONDATA_FIELD = protobuf.FieldDescriptor();
local PKRECORDSUBINFO_RECS_FIELD = protobuf.FieldDescriptor();

PKRECORDSUBINFO_POINT_FIELD.name = "point"
PKRECORDSUBINFO_POINT_FIELD.full_name = ".KKSG.PkRecordSubInfo.point"
PKRECORDSUBINFO_POINT_FIELD.number = 1
PKRECORDSUBINFO_POINT_FIELD.index = 0
PKRECORDSUBINFO_POINT_FIELD.label = 1
PKRECORDSUBINFO_POINT_FIELD.has_default_value = false
PKRECORDSUBINFO_POINT_FIELD.default_value = 0
PKRECORDSUBINFO_POINT_FIELD.type = 13
PKRECORDSUBINFO_POINT_FIELD.cpp_type = 3

PKRECORDSUBINFO_REWARDCOUNT_FIELD.name = "rewardcount"
PKRECORDSUBINFO_REWARDCOUNT_FIELD.full_name = ".KKSG.PkRecordSubInfo.rewardcount"
PKRECORDSUBINFO_REWARDCOUNT_FIELD.number = 2
PKRECORDSUBINFO_REWARDCOUNT_FIELD.index = 1
PKRECORDSUBINFO_REWARDCOUNT_FIELD.label = 1
PKRECORDSUBINFO_REWARDCOUNT_FIELD.has_default_value = false
PKRECORDSUBINFO_REWARDCOUNT_FIELD.default_value = 0
PKRECORDSUBINFO_REWARDCOUNT_FIELD.type = 13
PKRECORDSUBINFO_REWARDCOUNT_FIELD.cpp_type = 3

PKRECORDSUBINFO_SEASONDATA_FIELD.name = "seasondata"
PKRECORDSUBINFO_SEASONDATA_FIELD.full_name = ".KKSG.PkRecordSubInfo.seasondata"
PKRECORDSUBINFO_SEASONDATA_FIELD.number = 3
PKRECORDSUBINFO_SEASONDATA_FIELD.index = 2
PKRECORDSUBINFO_SEASONDATA_FIELD.label = 1
PKRECORDSUBINFO_SEASONDATA_FIELD.has_default_value = false
PKRECORDSUBINFO_SEASONDATA_FIELD.default_value = nil
PKRECORDSUBINFO_SEASONDATA_FIELD.message_type = PKBASEHIST_PB.PKBASEHIST
PKRECORDSUBINFO_SEASONDATA_FIELD.type = 11
PKRECORDSUBINFO_SEASONDATA_FIELD.cpp_type = 10

PKRECORDSUBINFO_RECS_FIELD.name = "recs"
PKRECORDSUBINFO_RECS_FIELD.full_name = ".KKSG.PkRecordSubInfo.recs"
PKRECORDSUBINFO_RECS_FIELD.number = 4
PKRECORDSUBINFO_RECS_FIELD.index = 3
PKRECORDSUBINFO_RECS_FIELD.label = 3
PKRECORDSUBINFO_RECS_FIELD.has_default_value = false
PKRECORDSUBINFO_RECS_FIELD.default_value = {}
PKRECORDSUBINFO_RECS_FIELD.message_type = PKONEREC_PB.PKONEREC
PKRECORDSUBINFO_RECS_FIELD.type = 11
PKRECORDSUBINFO_RECS_FIELD.cpp_type = 10

PKRECORDSUBINFO.name = "PkRecordSubInfo"
PKRECORDSUBINFO.full_name = ".KKSG.PkRecordSubInfo"
PKRECORDSUBINFO.nested_types = {}
PKRECORDSUBINFO.enum_types = {}
PKRECORDSUBINFO.fields = {PKRECORDSUBINFO_POINT_FIELD, PKRECORDSUBINFO_REWARDCOUNT_FIELD, PKRECORDSUBINFO_SEASONDATA_FIELD, PKRECORDSUBINFO_RECS_FIELD}
PKRECORDSUBINFO.is_extendable = false
PKRECORDSUBINFO.extensions = {}

PkRecordSubInfo = protobuf.Message(PKRECORDSUBINFO)


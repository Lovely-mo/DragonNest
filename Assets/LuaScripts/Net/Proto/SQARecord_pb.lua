-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
local MAPKEYVALUE_PB = require("MapKeyValue_pb")
module('SQARecord_pb')


SQARECORD = protobuf.Descriptor();
local SQARECORD_CUR_QA_TYPE_FIELD = protobuf.FieldDescriptor();
local SQARECORD_TRIGGER_TIME_FIELD = protobuf.FieldDescriptor();
local SQARECORD_USED_COUNT_FIELD = protobuf.FieldDescriptor();
local SQARECORD_LAST_RESET_TIME_FIELD = protobuf.FieldDescriptor();
local SQARECORD_LAST_END_TIME_FIELD = protobuf.FieldDescriptor();

SQARECORD_CUR_QA_TYPE_FIELD.name = "cur_qa_type"
SQARECORD_CUR_QA_TYPE_FIELD.full_name = ".KKSG.SQARecord.cur_qa_type"
SQARECORD_CUR_QA_TYPE_FIELD.number = 1
SQARECORD_CUR_QA_TYPE_FIELD.index = 0
SQARECORD_CUR_QA_TYPE_FIELD.label = 1
SQARECORD_CUR_QA_TYPE_FIELD.has_default_value = false
SQARECORD_CUR_QA_TYPE_FIELD.default_value = 0
SQARECORD_CUR_QA_TYPE_FIELD.type = 13
SQARECORD_CUR_QA_TYPE_FIELD.cpp_type = 3

SQARECORD_TRIGGER_TIME_FIELD.name = "trigger_time"
SQARECORD_TRIGGER_TIME_FIELD.full_name = ".KKSG.SQARecord.trigger_time"
SQARECORD_TRIGGER_TIME_FIELD.number = 2
SQARECORD_TRIGGER_TIME_FIELD.index = 1
SQARECORD_TRIGGER_TIME_FIELD.label = 3
SQARECORD_TRIGGER_TIME_FIELD.has_default_value = false
SQARECORD_TRIGGER_TIME_FIELD.default_value = {}
SQARECORD_TRIGGER_TIME_FIELD.message_type = MAPKEYVALUE_PB.MAPKEYVALUE
SQARECORD_TRIGGER_TIME_FIELD.type = 11
SQARECORD_TRIGGER_TIME_FIELD.cpp_type = 10

SQARECORD_USED_COUNT_FIELD.name = "used_count"
SQARECORD_USED_COUNT_FIELD.full_name = ".KKSG.SQARecord.used_count"
SQARECORD_USED_COUNT_FIELD.number = 3
SQARECORD_USED_COUNT_FIELD.index = 2
SQARECORD_USED_COUNT_FIELD.label = 3
SQARECORD_USED_COUNT_FIELD.has_default_value = false
SQARECORD_USED_COUNT_FIELD.default_value = {}
SQARECORD_USED_COUNT_FIELD.message_type = MAPKEYVALUE_PB.MAPKEYVALUE
SQARECORD_USED_COUNT_FIELD.type = 11
SQARECORD_USED_COUNT_FIELD.cpp_type = 10

SQARECORD_LAST_RESET_TIME_FIELD.name = "last_reset_time"
SQARECORD_LAST_RESET_TIME_FIELD.full_name = ".KKSG.SQARecord.last_reset_time"
SQARECORD_LAST_RESET_TIME_FIELD.number = 4
SQARECORD_LAST_RESET_TIME_FIELD.index = 3
SQARECORD_LAST_RESET_TIME_FIELD.label = 1
SQARECORD_LAST_RESET_TIME_FIELD.has_default_value = false
SQARECORD_LAST_RESET_TIME_FIELD.default_value = 0
SQARECORD_LAST_RESET_TIME_FIELD.type = 13
SQARECORD_LAST_RESET_TIME_FIELD.cpp_type = 3

SQARECORD_LAST_END_TIME_FIELD.name = "last_end_time"
SQARECORD_LAST_END_TIME_FIELD.full_name = ".KKSG.SQARecord.last_end_time"
SQARECORD_LAST_END_TIME_FIELD.number = 5
SQARECORD_LAST_END_TIME_FIELD.index = 4
SQARECORD_LAST_END_TIME_FIELD.label = 1
SQARECORD_LAST_END_TIME_FIELD.has_default_value = false
SQARECORD_LAST_END_TIME_FIELD.default_value = 0
SQARECORD_LAST_END_TIME_FIELD.type = 13
SQARECORD_LAST_END_TIME_FIELD.cpp_type = 3

SQARECORD.name = "SQARecord"
SQARECORD.full_name = ".KKSG.SQARecord"
SQARECORD.nested_types = {}
SQARECORD.enum_types = {}
SQARECORD.fields = {SQARECORD_CUR_QA_TYPE_FIELD, SQARECORD_TRIGGER_TIME_FIELD, SQARECORD_USED_COUNT_FIELD, SQARECORD_LAST_RESET_TIME_FIELD, SQARECORD_LAST_END_TIME_FIELD}
SQARECORD.is_extendable = false
SQARECORD.extensions = {}

SQARecord = protobuf.Message(SQARECORD)


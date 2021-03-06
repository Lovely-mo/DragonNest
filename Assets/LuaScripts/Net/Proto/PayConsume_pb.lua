-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
module('PayConsume_pb')


PAYCONSUME = protobuf.Descriptor();
local PAYCONSUME_LASTCONSUMETIME_FIELD = protobuf.FieldDescriptor();
local PAYCONSUME_CONSUMESCORE_FIELD = protobuf.FieldDescriptor();
local PAYCONSUME_MAXCONSUMELV_FIELD = protobuf.FieldDescriptor();
local PAYCONSUME_THISMONTHCOST_FIELD = protobuf.FieldDescriptor();
local PAYCONSUME_SETID_FIELD = protobuf.FieldDescriptor();
local PAYCONSUME_LASTCHECKDOWNTIME_FIELD = protobuf.FieldDescriptor();
local PAYCONSUME_ACTIVATEID_FIELD = protobuf.FieldDescriptor();

PAYCONSUME_LASTCONSUMETIME_FIELD.name = "lastconsumetime"
PAYCONSUME_LASTCONSUMETIME_FIELD.full_name = ".KKSG.PayConsume.lastconsumetime"
PAYCONSUME_LASTCONSUMETIME_FIELD.number = 1
PAYCONSUME_LASTCONSUMETIME_FIELD.index = 0
PAYCONSUME_LASTCONSUMETIME_FIELD.label = 1
PAYCONSUME_LASTCONSUMETIME_FIELD.has_default_value = false
PAYCONSUME_LASTCONSUMETIME_FIELD.default_value = 0
PAYCONSUME_LASTCONSUMETIME_FIELD.type = 13
PAYCONSUME_LASTCONSUMETIME_FIELD.cpp_type = 3

PAYCONSUME_CONSUMESCORE_FIELD.name = "consumescore"
PAYCONSUME_CONSUMESCORE_FIELD.full_name = ".KKSG.PayConsume.consumescore"
PAYCONSUME_CONSUMESCORE_FIELD.number = 2
PAYCONSUME_CONSUMESCORE_FIELD.index = 1
PAYCONSUME_CONSUMESCORE_FIELD.label = 1
PAYCONSUME_CONSUMESCORE_FIELD.has_default_value = false
PAYCONSUME_CONSUMESCORE_FIELD.default_value = 0
PAYCONSUME_CONSUMESCORE_FIELD.type = 13
PAYCONSUME_CONSUMESCORE_FIELD.cpp_type = 3

PAYCONSUME_MAXCONSUMELV_FIELD.name = "maxconsumelv"
PAYCONSUME_MAXCONSUMELV_FIELD.full_name = ".KKSG.PayConsume.maxconsumelv"
PAYCONSUME_MAXCONSUMELV_FIELD.number = 3
PAYCONSUME_MAXCONSUMELV_FIELD.index = 2
PAYCONSUME_MAXCONSUMELV_FIELD.label = 1
PAYCONSUME_MAXCONSUMELV_FIELD.has_default_value = false
PAYCONSUME_MAXCONSUMELV_FIELD.default_value = 0
PAYCONSUME_MAXCONSUMELV_FIELD.type = 13
PAYCONSUME_MAXCONSUMELV_FIELD.cpp_type = 3

PAYCONSUME_THISMONTHCOST_FIELD.name = "thismonthcost"
PAYCONSUME_THISMONTHCOST_FIELD.full_name = ".KKSG.PayConsume.thismonthcost"
PAYCONSUME_THISMONTHCOST_FIELD.number = 4
PAYCONSUME_THISMONTHCOST_FIELD.index = 3
PAYCONSUME_THISMONTHCOST_FIELD.label = 1
PAYCONSUME_THISMONTHCOST_FIELD.has_default_value = false
PAYCONSUME_THISMONTHCOST_FIELD.default_value = 0
PAYCONSUME_THISMONTHCOST_FIELD.type = 13
PAYCONSUME_THISMONTHCOST_FIELD.cpp_type = 3

PAYCONSUME_SETID_FIELD.name = "setid"
PAYCONSUME_SETID_FIELD.full_name = ".KKSG.PayConsume.setid"
PAYCONSUME_SETID_FIELD.number = 5
PAYCONSUME_SETID_FIELD.index = 4
PAYCONSUME_SETID_FIELD.label = 3
PAYCONSUME_SETID_FIELD.has_default_value = false
PAYCONSUME_SETID_FIELD.default_value = {}
PAYCONSUME_SETID_FIELD.type = 13
PAYCONSUME_SETID_FIELD.cpp_type = 3

PAYCONSUME_LASTCHECKDOWNTIME_FIELD.name = "lastcheckdowntime"
PAYCONSUME_LASTCHECKDOWNTIME_FIELD.full_name = ".KKSG.PayConsume.lastcheckdowntime"
PAYCONSUME_LASTCHECKDOWNTIME_FIELD.number = 6
PAYCONSUME_LASTCHECKDOWNTIME_FIELD.index = 5
PAYCONSUME_LASTCHECKDOWNTIME_FIELD.label = 1
PAYCONSUME_LASTCHECKDOWNTIME_FIELD.has_default_value = false
PAYCONSUME_LASTCHECKDOWNTIME_FIELD.default_value = 0
PAYCONSUME_LASTCHECKDOWNTIME_FIELD.type = 13
PAYCONSUME_LASTCHECKDOWNTIME_FIELD.cpp_type = 3

PAYCONSUME_ACTIVATEID_FIELD.name = "activateid"
PAYCONSUME_ACTIVATEID_FIELD.full_name = ".KKSG.PayConsume.activateid"
PAYCONSUME_ACTIVATEID_FIELD.number = 7
PAYCONSUME_ACTIVATEID_FIELD.index = 6
PAYCONSUME_ACTIVATEID_FIELD.label = 3
PAYCONSUME_ACTIVATEID_FIELD.has_default_value = false
PAYCONSUME_ACTIVATEID_FIELD.default_value = {}
PAYCONSUME_ACTIVATEID_FIELD.type = 13
PAYCONSUME_ACTIVATEID_FIELD.cpp_type = 3

PAYCONSUME.name = "PayConsume"
PAYCONSUME.full_name = ".KKSG.PayConsume"
PAYCONSUME.nested_types = {}
PAYCONSUME.enum_types = {}
PAYCONSUME.fields = {PAYCONSUME_LASTCONSUMETIME_FIELD, PAYCONSUME_CONSUMESCORE_FIELD, PAYCONSUME_MAXCONSUMELV_FIELD, PAYCONSUME_THISMONTHCOST_FIELD, PAYCONSUME_SETID_FIELD, PAYCONSUME_LASTCHECKDOWNTIME_FIELD, PAYCONSUME_ACTIVATEID_FIELD}
PAYCONSUME.is_extendable = false
PAYCONSUME.extensions = {}

PayConsume = protobuf.Message(PAYCONSUME)


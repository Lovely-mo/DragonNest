-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
module('CircleDrawData_pb')


CIRCLEDRAWDATA = protobuf.Descriptor();
local CIRCLEDRAWDATA_INDEX_FIELD = protobuf.FieldDescriptor();
local CIRCLEDRAWDATA_ITEMID_FIELD = protobuf.FieldDescriptor();
local CIRCLEDRAWDATA_ITEMCOUNT_FIELD = protobuf.FieldDescriptor();
local CIRCLEDRAWDATA_PROB_FIELD = protobuf.FieldDescriptor();

CIRCLEDRAWDATA_INDEX_FIELD.name = "index"
CIRCLEDRAWDATA_INDEX_FIELD.full_name = ".KKSG.CircleDrawData.index"
CIRCLEDRAWDATA_INDEX_FIELD.number = 1
CIRCLEDRAWDATA_INDEX_FIELD.index = 0
CIRCLEDRAWDATA_INDEX_FIELD.label = 1
CIRCLEDRAWDATA_INDEX_FIELD.has_default_value = false
CIRCLEDRAWDATA_INDEX_FIELD.default_value = 0
CIRCLEDRAWDATA_INDEX_FIELD.type = 13
CIRCLEDRAWDATA_INDEX_FIELD.cpp_type = 3

CIRCLEDRAWDATA_ITEMID_FIELD.name = "itemid"
CIRCLEDRAWDATA_ITEMID_FIELD.full_name = ".KKSG.CircleDrawData.itemid"
CIRCLEDRAWDATA_ITEMID_FIELD.number = 2
CIRCLEDRAWDATA_ITEMID_FIELD.index = 1
CIRCLEDRAWDATA_ITEMID_FIELD.label = 1
CIRCLEDRAWDATA_ITEMID_FIELD.has_default_value = false
CIRCLEDRAWDATA_ITEMID_FIELD.default_value = 0
CIRCLEDRAWDATA_ITEMID_FIELD.type = 13
CIRCLEDRAWDATA_ITEMID_FIELD.cpp_type = 3

CIRCLEDRAWDATA_ITEMCOUNT_FIELD.name = "itemcount"
CIRCLEDRAWDATA_ITEMCOUNT_FIELD.full_name = ".KKSG.CircleDrawData.itemcount"
CIRCLEDRAWDATA_ITEMCOUNT_FIELD.number = 3
CIRCLEDRAWDATA_ITEMCOUNT_FIELD.index = 2
CIRCLEDRAWDATA_ITEMCOUNT_FIELD.label = 1
CIRCLEDRAWDATA_ITEMCOUNT_FIELD.has_default_value = false
CIRCLEDRAWDATA_ITEMCOUNT_FIELD.default_value = 0
CIRCLEDRAWDATA_ITEMCOUNT_FIELD.type = 13
CIRCLEDRAWDATA_ITEMCOUNT_FIELD.cpp_type = 3

CIRCLEDRAWDATA_PROB_FIELD.name = "prob"
CIRCLEDRAWDATA_PROB_FIELD.full_name = ".KKSG.CircleDrawData.prob"
CIRCLEDRAWDATA_PROB_FIELD.number = 4
CIRCLEDRAWDATA_PROB_FIELD.index = 3
CIRCLEDRAWDATA_PROB_FIELD.label = 1
CIRCLEDRAWDATA_PROB_FIELD.has_default_value = false
CIRCLEDRAWDATA_PROB_FIELD.default_value = 0
CIRCLEDRAWDATA_PROB_FIELD.type = 13
CIRCLEDRAWDATA_PROB_FIELD.cpp_type = 3

CIRCLEDRAWDATA.name = "CircleDrawData"
CIRCLEDRAWDATA.full_name = ".KKSG.CircleDrawData"
CIRCLEDRAWDATA.nested_types = {}
CIRCLEDRAWDATA.enum_types = {}
CIRCLEDRAWDATA.fields = {CIRCLEDRAWDATA_INDEX_FIELD, CIRCLEDRAWDATA_ITEMID_FIELD, CIRCLEDRAWDATA_ITEMCOUNT_FIELD, CIRCLEDRAWDATA_PROB_FIELD}
CIRCLEDRAWDATA.is_extendable = false
CIRCLEDRAWDATA.extensions = {}

CircleDrawData = protobuf.Message(CIRCLEDRAWDATA)


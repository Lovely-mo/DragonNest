-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
module('atlasdata_pb')


ATLASDATA = protobuf.Descriptor();
local ATLASDATA_GROUPID_FIELD = protobuf.FieldDescriptor();
local ATLASDATA_FINISHID_FIELD = protobuf.FieldDescriptor();

ATLASDATA_GROUPID_FIELD.name = "groupid"
ATLASDATA_GROUPID_FIELD.full_name = ".KKSG.atlasdata.groupid"
ATLASDATA_GROUPID_FIELD.number = 1
ATLASDATA_GROUPID_FIELD.index = 0
ATLASDATA_GROUPID_FIELD.label = 1
ATLASDATA_GROUPID_FIELD.has_default_value = false
ATLASDATA_GROUPID_FIELD.default_value = 0
ATLASDATA_GROUPID_FIELD.type = 13
ATLASDATA_GROUPID_FIELD.cpp_type = 3

ATLASDATA_FINISHID_FIELD.name = "finishid"
ATLASDATA_FINISHID_FIELD.full_name = ".KKSG.atlasdata.finishid"
ATLASDATA_FINISHID_FIELD.number = 2
ATLASDATA_FINISHID_FIELD.index = 1
ATLASDATA_FINISHID_FIELD.label = 1
ATLASDATA_FINISHID_FIELD.has_default_value = false
ATLASDATA_FINISHID_FIELD.default_value = 0
ATLASDATA_FINISHID_FIELD.type = 13
ATLASDATA_FINISHID_FIELD.cpp_type = 3

ATLASDATA.name = "atlasdata"
ATLASDATA.full_name = ".KKSG.atlasdata"
ATLASDATA.nested_types = {}
ATLASDATA.enum_types = {}
ATLASDATA.fields = {ATLASDATA_GROUPID_FIELD, ATLASDATA_FINISHID_FIELD}
ATLASDATA.is_extendable = false
ATLASDATA.extensions = {}

atlasdata = protobuf.Message(ATLASDATA)


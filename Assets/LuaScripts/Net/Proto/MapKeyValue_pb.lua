-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
module('MapKeyValue_pb')


MAPKEYVALUE = protobuf.Descriptor();
local MAPKEYVALUE_KEY_FIELD = protobuf.FieldDescriptor();
local MAPKEYVALUE_VALUE_FIELD = protobuf.FieldDescriptor();

MAPKEYVALUE_KEY_FIELD.name = "key"
MAPKEYVALUE_KEY_FIELD.full_name = ".KKSG.MapKeyValue.key"
MAPKEYVALUE_KEY_FIELD.number = 1
MAPKEYVALUE_KEY_FIELD.index = 0
MAPKEYVALUE_KEY_FIELD.label = 1
MAPKEYVALUE_KEY_FIELD.has_default_value = false
MAPKEYVALUE_KEY_FIELD.default_value = 0
MAPKEYVALUE_KEY_FIELD.type = 4
MAPKEYVALUE_KEY_FIELD.cpp_type = 4

MAPKEYVALUE_VALUE_FIELD.name = "value"
MAPKEYVALUE_VALUE_FIELD.full_name = ".KKSG.MapKeyValue.value"
MAPKEYVALUE_VALUE_FIELD.number = 2
MAPKEYVALUE_VALUE_FIELD.index = 1
MAPKEYVALUE_VALUE_FIELD.label = 1
MAPKEYVALUE_VALUE_FIELD.has_default_value = false
MAPKEYVALUE_VALUE_FIELD.default_value = 0
MAPKEYVALUE_VALUE_FIELD.type = 4
MAPKEYVALUE_VALUE_FIELD.cpp_type = 4

MAPKEYVALUE.name = "MapKeyValue"
MAPKEYVALUE.full_name = ".KKSG.MapKeyValue"
MAPKEYVALUE.nested_types = {}
MAPKEYVALUE.enum_types = {}
MAPKEYVALUE.fields = {MAPKEYVALUE_KEY_FIELD, MAPKEYVALUE_VALUE_FIELD}
MAPKEYVALUE.is_extendable = false
MAPKEYVALUE.extensions = {}

MapKeyValue = protobuf.Message(MAPKEYVALUE)


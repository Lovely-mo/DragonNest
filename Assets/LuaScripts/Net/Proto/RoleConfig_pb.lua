-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
module('RoleConfig_pb')


ROLECONFIG = protobuf.Descriptor();
local ROLECONFIG_TYPE_FIELD = protobuf.FieldDescriptor();
local ROLECONFIG_VALUE_FIELD = protobuf.FieldDescriptor();

ROLECONFIG_TYPE_FIELD.name = "type"
ROLECONFIG_TYPE_FIELD.full_name = ".KKSG.RoleConfig.type"
ROLECONFIG_TYPE_FIELD.number = 1
ROLECONFIG_TYPE_FIELD.index = 0
ROLECONFIG_TYPE_FIELD.label = 3
ROLECONFIG_TYPE_FIELD.has_default_value = false
ROLECONFIG_TYPE_FIELD.default_value = {}
ROLECONFIG_TYPE_FIELD.type = 9
ROLECONFIG_TYPE_FIELD.cpp_type = 9

ROLECONFIG_VALUE_FIELD.name = "value"
ROLECONFIG_VALUE_FIELD.full_name = ".KKSG.RoleConfig.value"
ROLECONFIG_VALUE_FIELD.number = 2
ROLECONFIG_VALUE_FIELD.index = 1
ROLECONFIG_VALUE_FIELD.label = 3
ROLECONFIG_VALUE_FIELD.has_default_value = false
ROLECONFIG_VALUE_FIELD.default_value = {}
ROLECONFIG_VALUE_FIELD.type = 9
ROLECONFIG_VALUE_FIELD.cpp_type = 9

ROLECONFIG.name = "RoleConfig"
ROLECONFIG.full_name = ".KKSG.RoleConfig"
ROLECONFIG.nested_types = {}
ROLECONFIG.enum_types = {}
ROLECONFIG.fields = {ROLECONFIG_TYPE_FIELD, ROLECONFIG_VALUE_FIELD}
ROLECONFIG.is_extendable = false
ROLECONFIG.extensions = {}

RoleConfig = protobuf.Message(ROLECONFIG)


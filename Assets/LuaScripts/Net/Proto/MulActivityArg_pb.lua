-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
module('MulActivityArg_pb')


MULACTIVITYARG = protobuf.Descriptor();
local MULACTIVITYARG_ACID_FIELD = protobuf.FieldDescriptor();

MULACTIVITYARG_ACID_FIELD.name = "acid"
MULACTIVITYARG_ACID_FIELD.full_name = ".KKSG.MulActivityArg.acid"
MULACTIVITYARG_ACID_FIELD.number = 1
MULACTIVITYARG_ACID_FIELD.index = 0
MULACTIVITYARG_ACID_FIELD.label = 1
MULACTIVITYARG_ACID_FIELD.has_default_value = false
MULACTIVITYARG_ACID_FIELD.default_value = 0
MULACTIVITYARG_ACID_FIELD.type = 5
MULACTIVITYARG_ACID_FIELD.cpp_type = 1

MULACTIVITYARG.name = "MulActivityArg"
MULACTIVITYARG.full_name = ".KKSG.MulActivityArg"
MULACTIVITYARG.nested_types = {}
MULACTIVITYARG.enum_types = {}
MULACTIVITYARG.fields = {MULACTIVITYARG_ACID_FIELD}
MULACTIVITYARG.is_extendable = false
MULACTIVITYARG.extensions = {}

MulActivityArg = protobuf.Message(MULACTIVITYARG)

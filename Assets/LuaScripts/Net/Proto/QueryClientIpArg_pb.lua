-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
module('QueryClientIpArg_pb')


QUERYCLIENTIPARG = protobuf.Descriptor();
local QUERYCLIENTIPARG_PARAM_FIELD = protobuf.FieldDescriptor();

QUERYCLIENTIPARG_PARAM_FIELD.name = "param"
QUERYCLIENTIPARG_PARAM_FIELD.full_name = ".KKSG.QueryClientIpArg.param"
QUERYCLIENTIPARG_PARAM_FIELD.number = 1
QUERYCLIENTIPARG_PARAM_FIELD.index = 0
QUERYCLIENTIPARG_PARAM_FIELD.label = 1
QUERYCLIENTIPARG_PARAM_FIELD.has_default_value = false
QUERYCLIENTIPARG_PARAM_FIELD.default_value = 0
QUERYCLIENTIPARG_PARAM_FIELD.type = 13
QUERYCLIENTIPARG_PARAM_FIELD.cpp_type = 3

QUERYCLIENTIPARG.name = "QueryClientIpArg"
QUERYCLIENTIPARG.full_name = ".KKSG.QueryClientIpArg"
QUERYCLIENTIPARG.nested_types = {}
QUERYCLIENTIPARG.enum_types = {}
QUERYCLIENTIPARG.fields = {QUERYCLIENTIPARG_PARAM_FIELD}
QUERYCLIENTIPARG.is_extendable = false
QUERYCLIENTIPARG.extensions = {}

QueryClientIpArg = protobuf.Message(QUERYCLIENTIPARG)


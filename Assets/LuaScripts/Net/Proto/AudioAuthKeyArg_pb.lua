-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
module('AudioAuthKeyArg_pb')


AUDIOAUTHKEYARG = protobuf.Descriptor();
local AUDIOAUTHKEYARG_OPEN_ID_FIELD = protobuf.FieldDescriptor();
local AUDIOAUTHKEYARG_IP_FIELD = protobuf.FieldDescriptor();

AUDIOAUTHKEYARG_OPEN_ID_FIELD.name = "open_id"
AUDIOAUTHKEYARG_OPEN_ID_FIELD.full_name = ".KKSG.AudioAuthKeyArg.open_id"
AUDIOAUTHKEYARG_OPEN_ID_FIELD.number = 1
AUDIOAUTHKEYARG_OPEN_ID_FIELD.index = 0
AUDIOAUTHKEYARG_OPEN_ID_FIELD.label = 1
AUDIOAUTHKEYARG_OPEN_ID_FIELD.has_default_value = false
AUDIOAUTHKEYARG_OPEN_ID_FIELD.default_value = ""
AUDIOAUTHKEYARG_OPEN_ID_FIELD.type = 9
AUDIOAUTHKEYARG_OPEN_ID_FIELD.cpp_type = 9

AUDIOAUTHKEYARG_IP_FIELD.name = "ip"
AUDIOAUTHKEYARG_IP_FIELD.full_name = ".KKSG.AudioAuthKeyArg.ip"
AUDIOAUTHKEYARG_IP_FIELD.number = 2
AUDIOAUTHKEYARG_IP_FIELD.index = 1
AUDIOAUTHKEYARG_IP_FIELD.label = 1
AUDIOAUTHKEYARG_IP_FIELD.has_default_value = false
AUDIOAUTHKEYARG_IP_FIELD.default_value = ""
AUDIOAUTHKEYARG_IP_FIELD.type = 9
AUDIOAUTHKEYARG_IP_FIELD.cpp_type = 9

AUDIOAUTHKEYARG.name = "AudioAuthKeyArg"
AUDIOAUTHKEYARG.full_name = ".KKSG.AudioAuthKeyArg"
AUDIOAUTHKEYARG.nested_types = {}
AUDIOAUTHKEYARG.enum_types = {}
AUDIOAUTHKEYARG.fields = {AUDIOAUTHKEYARG_OPEN_ID_FIELD, AUDIOAUTHKEYARG_IP_FIELD}
AUDIOAUTHKEYARG.is_extendable = false
AUDIOAUTHKEYARG.extensions = {}

AudioAuthKeyArg = protobuf.Message(AUDIOAUTHKEYARG)

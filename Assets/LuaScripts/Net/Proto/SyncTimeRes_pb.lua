-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
module('SyncTimeRes_pb')


SYNCTIMERES = protobuf.Descriptor();
local SYNCTIMERES_SERVERTIME_FIELD = protobuf.FieldDescriptor();

SYNCTIMERES_SERVERTIME_FIELD.name = "serverTime"
SYNCTIMERES_SERVERTIME_FIELD.full_name = ".KKSG.SyncTimeRes.serverTime"
SYNCTIMERES_SERVERTIME_FIELD.number = 1
SYNCTIMERES_SERVERTIME_FIELD.index = 0
SYNCTIMERES_SERVERTIME_FIELD.label = 1
SYNCTIMERES_SERVERTIME_FIELD.has_default_value = false
SYNCTIMERES_SERVERTIME_FIELD.default_value = 0
SYNCTIMERES_SERVERTIME_FIELD.type = 3
SYNCTIMERES_SERVERTIME_FIELD.cpp_type = 2

SYNCTIMERES.name = "SyncTimeRes"
SYNCTIMERES.full_name = ".KKSG.SyncTimeRes"
SYNCTIMERES.nested_types = {}
SYNCTIMERES.enum_types = {}
SYNCTIMERES.fields = {SYNCTIMERES_SERVERTIME_FIELD}
SYNCTIMERES.is_extendable = false
SYNCTIMERES.extensions = {}

SyncTimeRes = protobuf.Message(SYNCTIMERES)


-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
module('PandoraDrop_pb')


PANDORADROP = protobuf.Descriptor();
local PANDORADROP_PANDORADROPID_FIELD = protobuf.FieldDescriptor();
local PANDORADROP_BETTERDROPTIMES_FIELD = protobuf.FieldDescriptor();
local PANDORADROP_NOUSEDBESTDROPTIMES_FIELD = protobuf.FieldDescriptor();
local PANDORADROP_NEXTBETTERDROPTIMES_FIELD = protobuf.FieldDescriptor();
local PANDORADROP_NOUSEDNEXTBESTDROPTIMES_FIELD = protobuf.FieldDescriptor();
local PANDORADROP_BESTDROPTIMES_FIELD = protobuf.FieldDescriptor();
local PANDORADROP_NEXTBESTDROPTIMES_FIELD = protobuf.FieldDescriptor();

PANDORADROP_PANDORADROPID_FIELD.name = "pandoraDropID"
PANDORADROP_PANDORADROPID_FIELD.full_name = ".KKSG.PandoraDrop.pandoraDropID"
PANDORADROP_PANDORADROPID_FIELD.number = 1
PANDORADROP_PANDORADROPID_FIELD.index = 0
PANDORADROP_PANDORADROPID_FIELD.label = 1
PANDORADROP_PANDORADROPID_FIELD.has_default_value = false
PANDORADROP_PANDORADROPID_FIELD.default_value = 0
PANDORADROP_PANDORADROPID_FIELD.type = 13
PANDORADROP_PANDORADROPID_FIELD.cpp_type = 3

PANDORADROP_BETTERDROPTIMES_FIELD.name = "betterDropTimes"
PANDORADROP_BETTERDROPTIMES_FIELD.full_name = ".KKSG.PandoraDrop.betterDropTimes"
PANDORADROP_BETTERDROPTIMES_FIELD.number = 2
PANDORADROP_BETTERDROPTIMES_FIELD.index = 1
PANDORADROP_BETTERDROPTIMES_FIELD.label = 1
PANDORADROP_BETTERDROPTIMES_FIELD.has_default_value = false
PANDORADROP_BETTERDROPTIMES_FIELD.default_value = 0
PANDORADROP_BETTERDROPTIMES_FIELD.type = 13
PANDORADROP_BETTERDROPTIMES_FIELD.cpp_type = 3

PANDORADROP_NOUSEDBESTDROPTIMES_FIELD.name = "noUsedBestDropTimes"
PANDORADROP_NOUSEDBESTDROPTIMES_FIELD.full_name = ".KKSG.PandoraDrop.noUsedBestDropTimes"
PANDORADROP_NOUSEDBESTDROPTIMES_FIELD.number = 3
PANDORADROP_NOUSEDBESTDROPTIMES_FIELD.index = 2
PANDORADROP_NOUSEDBESTDROPTIMES_FIELD.label = 1
PANDORADROP_NOUSEDBESTDROPTIMES_FIELD.has_default_value = false
PANDORADROP_NOUSEDBESTDROPTIMES_FIELD.default_value = 0
PANDORADROP_NOUSEDBESTDROPTIMES_FIELD.type = 13
PANDORADROP_NOUSEDBESTDROPTIMES_FIELD.cpp_type = 3

PANDORADROP_NEXTBETTERDROPTIMES_FIELD.name = "nextBetterDropTimes"
PANDORADROP_NEXTBETTERDROPTIMES_FIELD.full_name = ".KKSG.PandoraDrop.nextBetterDropTimes"
PANDORADROP_NEXTBETTERDROPTIMES_FIELD.number = 4
PANDORADROP_NEXTBETTERDROPTIMES_FIELD.index = 3
PANDORADROP_NEXTBETTERDROPTIMES_FIELD.label = 1
PANDORADROP_NEXTBETTERDROPTIMES_FIELD.has_default_value = false
PANDORADROP_NEXTBETTERDROPTIMES_FIELD.default_value = 0
PANDORADROP_NEXTBETTERDROPTIMES_FIELD.type = 13
PANDORADROP_NEXTBETTERDROPTIMES_FIELD.cpp_type = 3

PANDORADROP_NOUSEDNEXTBESTDROPTIMES_FIELD.name = "noUsedNextBestDropTimes"
PANDORADROP_NOUSEDNEXTBESTDROPTIMES_FIELD.full_name = ".KKSG.PandoraDrop.noUsedNextBestDropTimes"
PANDORADROP_NOUSEDNEXTBESTDROPTIMES_FIELD.number = 5
PANDORADROP_NOUSEDNEXTBESTDROPTIMES_FIELD.index = 4
PANDORADROP_NOUSEDNEXTBESTDROPTIMES_FIELD.label = 1
PANDORADROP_NOUSEDNEXTBESTDROPTIMES_FIELD.has_default_value = false
PANDORADROP_NOUSEDNEXTBESTDROPTIMES_FIELD.default_value = 0
PANDORADROP_NOUSEDNEXTBESTDROPTIMES_FIELD.type = 13
PANDORADROP_NOUSEDNEXTBESTDROPTIMES_FIELD.cpp_type = 3

PANDORADROP_BESTDROPTIMES_FIELD.name = "bestDropTimes"
PANDORADROP_BESTDROPTIMES_FIELD.full_name = ".KKSG.PandoraDrop.bestDropTimes"
PANDORADROP_BESTDROPTIMES_FIELD.number = 6
PANDORADROP_BESTDROPTIMES_FIELD.index = 5
PANDORADROP_BESTDROPTIMES_FIELD.label = 3
PANDORADROP_BESTDROPTIMES_FIELD.has_default_value = false
PANDORADROP_BESTDROPTIMES_FIELD.default_value = {}
PANDORADROP_BESTDROPTIMES_FIELD.type = 13
PANDORADROP_BESTDROPTIMES_FIELD.cpp_type = 3

PANDORADROP_NEXTBESTDROPTIMES_FIELD.name = "nextBestDropTimes"
PANDORADROP_NEXTBESTDROPTIMES_FIELD.full_name = ".KKSG.PandoraDrop.nextBestDropTimes"
PANDORADROP_NEXTBESTDROPTIMES_FIELD.number = 7
PANDORADROP_NEXTBESTDROPTIMES_FIELD.index = 6
PANDORADROP_NEXTBESTDROPTIMES_FIELD.label = 3
PANDORADROP_NEXTBESTDROPTIMES_FIELD.has_default_value = false
PANDORADROP_NEXTBESTDROPTIMES_FIELD.default_value = {}
PANDORADROP_NEXTBESTDROPTIMES_FIELD.type = 13
PANDORADROP_NEXTBESTDROPTIMES_FIELD.cpp_type = 3

PANDORADROP.name = "PandoraDrop"
PANDORADROP.full_name = ".KKSG.PandoraDrop"
PANDORADROP.nested_types = {}
PANDORADROP.enum_types = {}
PANDORADROP.fields = {PANDORADROP_PANDORADROPID_FIELD, PANDORADROP_BETTERDROPTIMES_FIELD, PANDORADROP_NOUSEDBESTDROPTIMES_FIELD, PANDORADROP_NEXTBETTERDROPTIMES_FIELD, PANDORADROP_NOUSEDNEXTBESTDROPTIMES_FIELD, PANDORADROP_BESTDROPTIMES_FIELD, PANDORADROP_NEXTBESTDROPTIMES_FIELD}
PANDORADROP.is_extendable = false
PANDORADROP.extensions = {}

PandoraDrop = protobuf.Message(PANDORADROP)


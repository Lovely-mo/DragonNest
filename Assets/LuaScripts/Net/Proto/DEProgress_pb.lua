-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
local DEPROGRESSSTATE_PB = require("DEProgressState_pb")
module('DEProgress_pb')


DEPROGRESS = protobuf.Descriptor();
local DEPROGRESS_SCENEID_FIELD = protobuf.FieldDescriptor();
local DEPROGRESS_BOSSAVGHPPERCENT_FIELD = protobuf.FieldDescriptor();
local DEPROGRESS_STATE_FIELD = protobuf.FieldDescriptor();

DEPROGRESS_SCENEID_FIELD.name = "sceneID"
DEPROGRESS_SCENEID_FIELD.full_name = ".KKSG.DEProgress.sceneID"
DEPROGRESS_SCENEID_FIELD.number = 1
DEPROGRESS_SCENEID_FIELD.index = 0
DEPROGRESS_SCENEID_FIELD.label = 1
DEPROGRESS_SCENEID_FIELD.has_default_value = false
DEPROGRESS_SCENEID_FIELD.default_value = 0
DEPROGRESS_SCENEID_FIELD.type = 13
DEPROGRESS_SCENEID_FIELD.cpp_type = 3

DEPROGRESS_BOSSAVGHPPERCENT_FIELD.name = "bossavghppercent"
DEPROGRESS_BOSSAVGHPPERCENT_FIELD.full_name = ".KKSG.DEProgress.bossavghppercent"
DEPROGRESS_BOSSAVGHPPERCENT_FIELD.number = 2
DEPROGRESS_BOSSAVGHPPERCENT_FIELD.index = 1
DEPROGRESS_BOSSAVGHPPERCENT_FIELD.label = 1
DEPROGRESS_BOSSAVGHPPERCENT_FIELD.has_default_value = false
DEPROGRESS_BOSSAVGHPPERCENT_FIELD.default_value = 0
DEPROGRESS_BOSSAVGHPPERCENT_FIELD.type = 5
DEPROGRESS_BOSSAVGHPPERCENT_FIELD.cpp_type = 1

DEPROGRESS_STATE_FIELD.name = "state"
DEPROGRESS_STATE_FIELD.full_name = ".KKSG.DEProgress.state"
DEPROGRESS_STATE_FIELD.number = 3
DEPROGRESS_STATE_FIELD.index = 2
DEPROGRESS_STATE_FIELD.label = 1
DEPROGRESS_STATE_FIELD.has_default_value = false
DEPROGRESS_STATE_FIELD.default_value = nil
DEPROGRESS_STATE_FIELD.enum_type = DEPROGRESSSTATE_PB.DEPROGRESSSTATE
DEPROGRESS_STATE_FIELD.type = 14
DEPROGRESS_STATE_FIELD.cpp_type = 8

DEPROGRESS.name = "DEProgress"
DEPROGRESS.full_name = ".KKSG.DEProgress"
DEPROGRESS.nested_types = {}
DEPROGRESS.enum_types = {}
DEPROGRESS.fields = {DEPROGRESS_SCENEID_FIELD, DEPROGRESS_BOSSAVGHPPERCENT_FIELD, DEPROGRESS_STATE_FIELD}
DEPROGRESS.is_extendable = false
DEPROGRESS.extensions = {}

DEProgress = protobuf.Message(DEPROGRESS)


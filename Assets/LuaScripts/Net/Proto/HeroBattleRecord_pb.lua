-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
local HEROBATTLEONEGAME_PB = require("HeroBattleOneGame_pb")
module('HeroBattleRecord_pb')


HEROBATTLERECORD = protobuf.Descriptor();
local HEROBATTLERECORD_HAVEHERO_FIELD = protobuf.FieldDescriptor();
local HEROBATTLERECORD_CANGETPRIZE_FIELD = protobuf.FieldDescriptor();
local HEROBATTLERECORD_ALREADYGETPRIZE_FIELD = protobuf.FieldDescriptor();
local HEROBATTLERECORD_TOTALNUM_FIELD = protobuf.FieldDescriptor();
local HEROBATTLERECORD_WINNUM_FIELD = protobuf.FieldDescriptor();
local HEROBATTLERECORD_LOSENUM_FIELD = protobuf.FieldDescriptor();
local HEROBATTLERECORD_WINTHISWEEK_FIELD = protobuf.FieldDescriptor();
local HEROBATTLERECORD_LASTUPDATETIME_FIELD = protobuf.FieldDescriptor();
local HEROBATTLERECORD_GAMERECORD_FIELD = protobuf.FieldDescriptor();
local HEROBATTLERECORD_TODAYSPCOUNT_FIELD = protobuf.FieldDescriptor();
local HEROBATTLERECORD_FREEWEEKHERO_FIELD = protobuf.FieldDescriptor();
local HEROBATTLERECORD_BIGREWARDCOUNT_FIELD = protobuf.FieldDescriptor();
local HEROBATTLERECORD_WEEKPRIZE_FIELD = protobuf.FieldDescriptor();
local HEROBATTLERECORD_ELOPOINT_FIELD = protobuf.FieldDescriptor();
local HEROBATTLERECORD_DAYTIME_FIELD = protobuf.FieldDescriptor();
local HEROBATTLERECORD_DAYTIMES_FIELD = protobuf.FieldDescriptor();
local HEROBATTLERECORD_EXPERIENCEHERO_FIELD = protobuf.FieldDescriptor();
local HEROBATTLERECORD_EXPERIENCEHEROTIME_FIELD = protobuf.FieldDescriptor();
local HEROBATTLERECORD_CONTINUEWINNUM_FIELD = protobuf.FieldDescriptor();
local HEROBATTLERECORD_MAXKILLNUM_FIELD = protobuf.FieldDescriptor();

HEROBATTLERECORD_HAVEHERO_FIELD.name = "havehero"
HEROBATTLERECORD_HAVEHERO_FIELD.full_name = ".KKSG.HeroBattleRecord.havehero"
HEROBATTLERECORD_HAVEHERO_FIELD.number = 1
HEROBATTLERECORD_HAVEHERO_FIELD.index = 0
HEROBATTLERECORD_HAVEHERO_FIELD.label = 3
HEROBATTLERECORD_HAVEHERO_FIELD.has_default_value = false
HEROBATTLERECORD_HAVEHERO_FIELD.default_value = {}
HEROBATTLERECORD_HAVEHERO_FIELD.type = 13
HEROBATTLERECORD_HAVEHERO_FIELD.cpp_type = 3

HEROBATTLERECORD_CANGETPRIZE_FIELD.name = "cangetprize"
HEROBATTLERECORD_CANGETPRIZE_FIELD.full_name = ".KKSG.HeroBattleRecord.cangetprize"
HEROBATTLERECORD_CANGETPRIZE_FIELD.number = 2
HEROBATTLERECORD_CANGETPRIZE_FIELD.index = 1
HEROBATTLERECORD_CANGETPRIZE_FIELD.label = 1
HEROBATTLERECORD_CANGETPRIZE_FIELD.has_default_value = false
HEROBATTLERECORD_CANGETPRIZE_FIELD.default_value = false
HEROBATTLERECORD_CANGETPRIZE_FIELD.type = 8
HEROBATTLERECORD_CANGETPRIZE_FIELD.cpp_type = 7

HEROBATTLERECORD_ALREADYGETPRIZE_FIELD.name = "alreadygetprize"
HEROBATTLERECORD_ALREADYGETPRIZE_FIELD.full_name = ".KKSG.HeroBattleRecord.alreadygetprize"
HEROBATTLERECORD_ALREADYGETPRIZE_FIELD.number = 3
HEROBATTLERECORD_ALREADYGETPRIZE_FIELD.index = 2
HEROBATTLERECORD_ALREADYGETPRIZE_FIELD.label = 1
HEROBATTLERECORD_ALREADYGETPRIZE_FIELD.has_default_value = false
HEROBATTLERECORD_ALREADYGETPRIZE_FIELD.default_value = false
HEROBATTLERECORD_ALREADYGETPRIZE_FIELD.type = 8
HEROBATTLERECORD_ALREADYGETPRIZE_FIELD.cpp_type = 7

HEROBATTLERECORD_TOTALNUM_FIELD.name = "totalnum"
HEROBATTLERECORD_TOTALNUM_FIELD.full_name = ".KKSG.HeroBattleRecord.totalnum"
HEROBATTLERECORD_TOTALNUM_FIELD.number = 4
HEROBATTLERECORD_TOTALNUM_FIELD.index = 3
HEROBATTLERECORD_TOTALNUM_FIELD.label = 1
HEROBATTLERECORD_TOTALNUM_FIELD.has_default_value = false
HEROBATTLERECORD_TOTALNUM_FIELD.default_value = 0
HEROBATTLERECORD_TOTALNUM_FIELD.type = 13
HEROBATTLERECORD_TOTALNUM_FIELD.cpp_type = 3

HEROBATTLERECORD_WINNUM_FIELD.name = "winnum"
HEROBATTLERECORD_WINNUM_FIELD.full_name = ".KKSG.HeroBattleRecord.winnum"
HEROBATTLERECORD_WINNUM_FIELD.number = 5
HEROBATTLERECORD_WINNUM_FIELD.index = 4
HEROBATTLERECORD_WINNUM_FIELD.label = 1
HEROBATTLERECORD_WINNUM_FIELD.has_default_value = false
HEROBATTLERECORD_WINNUM_FIELD.default_value = 0
HEROBATTLERECORD_WINNUM_FIELD.type = 13
HEROBATTLERECORD_WINNUM_FIELD.cpp_type = 3

HEROBATTLERECORD_LOSENUM_FIELD.name = "losenum"
HEROBATTLERECORD_LOSENUM_FIELD.full_name = ".KKSG.HeroBattleRecord.losenum"
HEROBATTLERECORD_LOSENUM_FIELD.number = 6
HEROBATTLERECORD_LOSENUM_FIELD.index = 5
HEROBATTLERECORD_LOSENUM_FIELD.label = 1
HEROBATTLERECORD_LOSENUM_FIELD.has_default_value = false
HEROBATTLERECORD_LOSENUM_FIELD.default_value = 0
HEROBATTLERECORD_LOSENUM_FIELD.type = 13
HEROBATTLERECORD_LOSENUM_FIELD.cpp_type = 3

HEROBATTLERECORD_WINTHISWEEK_FIELD.name = "winthisweek"
HEROBATTLERECORD_WINTHISWEEK_FIELD.full_name = ".KKSG.HeroBattleRecord.winthisweek"
HEROBATTLERECORD_WINTHISWEEK_FIELD.number = 7
HEROBATTLERECORD_WINTHISWEEK_FIELD.index = 6
HEROBATTLERECORD_WINTHISWEEK_FIELD.label = 1
HEROBATTLERECORD_WINTHISWEEK_FIELD.has_default_value = false
HEROBATTLERECORD_WINTHISWEEK_FIELD.default_value = 0
HEROBATTLERECORD_WINTHISWEEK_FIELD.type = 13
HEROBATTLERECORD_WINTHISWEEK_FIELD.cpp_type = 3

HEROBATTLERECORD_LASTUPDATETIME_FIELD.name = "lastupdatetime"
HEROBATTLERECORD_LASTUPDATETIME_FIELD.full_name = ".KKSG.HeroBattleRecord.lastupdatetime"
HEROBATTLERECORD_LASTUPDATETIME_FIELD.number = 8
HEROBATTLERECORD_LASTUPDATETIME_FIELD.index = 7
HEROBATTLERECORD_LASTUPDATETIME_FIELD.label = 1
HEROBATTLERECORD_LASTUPDATETIME_FIELD.has_default_value = false
HEROBATTLERECORD_LASTUPDATETIME_FIELD.default_value = 0
HEROBATTLERECORD_LASTUPDATETIME_FIELD.type = 13
HEROBATTLERECORD_LASTUPDATETIME_FIELD.cpp_type = 3

HEROBATTLERECORD_GAMERECORD_FIELD.name = "gamerecord"
HEROBATTLERECORD_GAMERECORD_FIELD.full_name = ".KKSG.HeroBattleRecord.gamerecord"
HEROBATTLERECORD_GAMERECORD_FIELD.number = 9
HEROBATTLERECORD_GAMERECORD_FIELD.index = 8
HEROBATTLERECORD_GAMERECORD_FIELD.label = 3
HEROBATTLERECORD_GAMERECORD_FIELD.has_default_value = false
HEROBATTLERECORD_GAMERECORD_FIELD.default_value = {}
HEROBATTLERECORD_GAMERECORD_FIELD.message_type = HEROBATTLEONEGAME_PB.HEROBATTLEONEGAME
HEROBATTLERECORD_GAMERECORD_FIELD.type = 11
HEROBATTLERECORD_GAMERECORD_FIELD.cpp_type = 10

HEROBATTLERECORD_TODAYSPCOUNT_FIELD.name = "todayspcount"
HEROBATTLERECORD_TODAYSPCOUNT_FIELD.full_name = ".KKSG.HeroBattleRecord.todayspcount"
HEROBATTLERECORD_TODAYSPCOUNT_FIELD.number = 10
HEROBATTLERECORD_TODAYSPCOUNT_FIELD.index = 9
HEROBATTLERECORD_TODAYSPCOUNT_FIELD.label = 1
HEROBATTLERECORD_TODAYSPCOUNT_FIELD.has_default_value = false
HEROBATTLERECORD_TODAYSPCOUNT_FIELD.default_value = 0
HEROBATTLERECORD_TODAYSPCOUNT_FIELD.type = 13
HEROBATTLERECORD_TODAYSPCOUNT_FIELD.cpp_type = 3

HEROBATTLERECORD_FREEWEEKHERO_FIELD.name = "freeweekhero"
HEROBATTLERECORD_FREEWEEKHERO_FIELD.full_name = ".KKSG.HeroBattleRecord.freeweekhero"
HEROBATTLERECORD_FREEWEEKHERO_FIELD.number = 11
HEROBATTLERECORD_FREEWEEKHERO_FIELD.index = 10
HEROBATTLERECORD_FREEWEEKHERO_FIELD.label = 3
HEROBATTLERECORD_FREEWEEKHERO_FIELD.has_default_value = false
HEROBATTLERECORD_FREEWEEKHERO_FIELD.default_value = {}
HEROBATTLERECORD_FREEWEEKHERO_FIELD.type = 13
HEROBATTLERECORD_FREEWEEKHERO_FIELD.cpp_type = 3

HEROBATTLERECORD_BIGREWARDCOUNT_FIELD.name = "bigrewardcount"
HEROBATTLERECORD_BIGREWARDCOUNT_FIELD.full_name = ".KKSG.HeroBattleRecord.bigrewardcount"
HEROBATTLERECORD_BIGREWARDCOUNT_FIELD.number = 12
HEROBATTLERECORD_BIGREWARDCOUNT_FIELD.index = 11
HEROBATTLERECORD_BIGREWARDCOUNT_FIELD.label = 1
HEROBATTLERECORD_BIGREWARDCOUNT_FIELD.has_default_value = false
HEROBATTLERECORD_BIGREWARDCOUNT_FIELD.default_value = 0
HEROBATTLERECORD_BIGREWARDCOUNT_FIELD.type = 13
HEROBATTLERECORD_BIGREWARDCOUNT_FIELD.cpp_type = 3

HEROBATTLERECORD_WEEKPRIZE_FIELD.name = "weekprize"
HEROBATTLERECORD_WEEKPRIZE_FIELD.full_name = ".KKSG.HeroBattleRecord.weekprize"
HEROBATTLERECORD_WEEKPRIZE_FIELD.number = 13
HEROBATTLERECORD_WEEKPRIZE_FIELD.index = 12
HEROBATTLERECORD_WEEKPRIZE_FIELD.label = 1
HEROBATTLERECORD_WEEKPRIZE_FIELD.has_default_value = false
HEROBATTLERECORD_WEEKPRIZE_FIELD.default_value = 0
HEROBATTLERECORD_WEEKPRIZE_FIELD.type = 13
HEROBATTLERECORD_WEEKPRIZE_FIELD.cpp_type = 3

HEROBATTLERECORD_ELOPOINT_FIELD.name = "elopoint"
HEROBATTLERECORD_ELOPOINT_FIELD.full_name = ".KKSG.HeroBattleRecord.elopoint"
HEROBATTLERECORD_ELOPOINT_FIELD.number = 14
HEROBATTLERECORD_ELOPOINT_FIELD.index = 13
HEROBATTLERECORD_ELOPOINT_FIELD.label = 1
HEROBATTLERECORD_ELOPOINT_FIELD.has_default_value = false
HEROBATTLERECORD_ELOPOINT_FIELD.default_value = 0.0
HEROBATTLERECORD_ELOPOINT_FIELD.type = 1
HEROBATTLERECORD_ELOPOINT_FIELD.cpp_type = 5

HEROBATTLERECORD_DAYTIME_FIELD.name = "daytime"
HEROBATTLERECORD_DAYTIME_FIELD.full_name = ".KKSG.HeroBattleRecord.daytime"
HEROBATTLERECORD_DAYTIME_FIELD.number = 15
HEROBATTLERECORD_DAYTIME_FIELD.index = 14
HEROBATTLERECORD_DAYTIME_FIELD.label = 1
HEROBATTLERECORD_DAYTIME_FIELD.has_default_value = false
HEROBATTLERECORD_DAYTIME_FIELD.default_value = 0
HEROBATTLERECORD_DAYTIME_FIELD.type = 13
HEROBATTLERECORD_DAYTIME_FIELD.cpp_type = 3

HEROBATTLERECORD_DAYTIMES_FIELD.name = "daytimes"
HEROBATTLERECORD_DAYTIMES_FIELD.full_name = ".KKSG.HeroBattleRecord.daytimes"
HEROBATTLERECORD_DAYTIMES_FIELD.number = 16
HEROBATTLERECORD_DAYTIMES_FIELD.index = 15
HEROBATTLERECORD_DAYTIMES_FIELD.label = 1
HEROBATTLERECORD_DAYTIMES_FIELD.has_default_value = false
HEROBATTLERECORD_DAYTIMES_FIELD.default_value = 0
HEROBATTLERECORD_DAYTIMES_FIELD.type = 13
HEROBATTLERECORD_DAYTIMES_FIELD.cpp_type = 3

HEROBATTLERECORD_EXPERIENCEHERO_FIELD.name = "experiencehero"
HEROBATTLERECORD_EXPERIENCEHERO_FIELD.full_name = ".KKSG.HeroBattleRecord.experiencehero"
HEROBATTLERECORD_EXPERIENCEHERO_FIELD.number = 17
HEROBATTLERECORD_EXPERIENCEHERO_FIELD.index = 16
HEROBATTLERECORD_EXPERIENCEHERO_FIELD.label = 3
HEROBATTLERECORD_EXPERIENCEHERO_FIELD.has_default_value = false
HEROBATTLERECORD_EXPERIENCEHERO_FIELD.default_value = {}
HEROBATTLERECORD_EXPERIENCEHERO_FIELD.type = 13
HEROBATTLERECORD_EXPERIENCEHERO_FIELD.cpp_type = 3

HEROBATTLERECORD_EXPERIENCEHEROTIME_FIELD.name = "experienceherotime"
HEROBATTLERECORD_EXPERIENCEHEROTIME_FIELD.full_name = ".KKSG.HeroBattleRecord.experienceherotime"
HEROBATTLERECORD_EXPERIENCEHEROTIME_FIELD.number = 18
HEROBATTLERECORD_EXPERIENCEHEROTIME_FIELD.index = 17
HEROBATTLERECORD_EXPERIENCEHEROTIME_FIELD.label = 3
HEROBATTLERECORD_EXPERIENCEHEROTIME_FIELD.has_default_value = false
HEROBATTLERECORD_EXPERIENCEHEROTIME_FIELD.default_value = {}
HEROBATTLERECORD_EXPERIENCEHEROTIME_FIELD.type = 13
HEROBATTLERECORD_EXPERIENCEHEROTIME_FIELD.cpp_type = 3

HEROBATTLERECORD_CONTINUEWINNUM_FIELD.name = "continuewinnum"
HEROBATTLERECORD_CONTINUEWINNUM_FIELD.full_name = ".KKSG.HeroBattleRecord.continuewinnum"
HEROBATTLERECORD_CONTINUEWINNUM_FIELD.number = 19
HEROBATTLERECORD_CONTINUEWINNUM_FIELD.index = 18
HEROBATTLERECORD_CONTINUEWINNUM_FIELD.label = 1
HEROBATTLERECORD_CONTINUEWINNUM_FIELD.has_default_value = false
HEROBATTLERECORD_CONTINUEWINNUM_FIELD.default_value = 0
HEROBATTLERECORD_CONTINUEWINNUM_FIELD.type = 13
HEROBATTLERECORD_CONTINUEWINNUM_FIELD.cpp_type = 3

HEROBATTLERECORD_MAXKILLNUM_FIELD.name = "maxkillnum"
HEROBATTLERECORD_MAXKILLNUM_FIELD.full_name = ".KKSG.HeroBattleRecord.maxkillnum"
HEROBATTLERECORD_MAXKILLNUM_FIELD.number = 20
HEROBATTLERECORD_MAXKILLNUM_FIELD.index = 19
HEROBATTLERECORD_MAXKILLNUM_FIELD.label = 1
HEROBATTLERECORD_MAXKILLNUM_FIELD.has_default_value = false
HEROBATTLERECORD_MAXKILLNUM_FIELD.default_value = 0
HEROBATTLERECORD_MAXKILLNUM_FIELD.type = 13
HEROBATTLERECORD_MAXKILLNUM_FIELD.cpp_type = 3

HEROBATTLERECORD.name = "HeroBattleRecord"
HEROBATTLERECORD.full_name = ".KKSG.HeroBattleRecord"
HEROBATTLERECORD.nested_types = {}
HEROBATTLERECORD.enum_types = {}
HEROBATTLERECORD.fields = {HEROBATTLERECORD_HAVEHERO_FIELD, HEROBATTLERECORD_CANGETPRIZE_FIELD, HEROBATTLERECORD_ALREADYGETPRIZE_FIELD, HEROBATTLERECORD_TOTALNUM_FIELD, HEROBATTLERECORD_WINNUM_FIELD, HEROBATTLERECORD_LOSENUM_FIELD, HEROBATTLERECORD_WINTHISWEEK_FIELD, HEROBATTLERECORD_LASTUPDATETIME_FIELD, HEROBATTLERECORD_GAMERECORD_FIELD, HEROBATTLERECORD_TODAYSPCOUNT_FIELD, HEROBATTLERECORD_FREEWEEKHERO_FIELD, HEROBATTLERECORD_BIGREWARDCOUNT_FIELD, HEROBATTLERECORD_WEEKPRIZE_FIELD, HEROBATTLERECORD_ELOPOINT_FIELD, HEROBATTLERECORD_DAYTIME_FIELD, HEROBATTLERECORD_DAYTIMES_FIELD, HEROBATTLERECORD_EXPERIENCEHERO_FIELD, HEROBATTLERECORD_EXPERIENCEHEROTIME_FIELD, HEROBATTLERECORD_CONTINUEWINNUM_FIELD, HEROBATTLERECORD_MAXKILLNUM_FIELD}
HEROBATTLERECORD.is_extendable = false
HEROBATTLERECORD.extensions = {}

HeroBattleRecord = protobuf.Message(HEROBATTLERECORD)


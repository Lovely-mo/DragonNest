-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
local ITEM_PB = require("Item_pb")
local ITEMBRIEF_PB = require("ItemBrief_pb")
module('ShopRecordOne_pb')


SHOPRECORDONE = protobuf.Descriptor();
local SHOPRECORDONE_TYPE_FIELD = protobuf.FieldDescriptor();
local SHOPRECORDONE_UPDATETIME_FIELD = protobuf.FieldDescriptor();
local SHOPRECORDONE_ITEMS_FIELD = protobuf.FieldDescriptor();
local SHOPRECORDONE_SLOTS_FIELD = protobuf.FieldDescriptor();
local SHOPRECORDONE_BUYCOUNT_FIELD = protobuf.FieldDescriptor();
local SHOPRECORDONE_DAILYBUYCOUNT_FIELD = protobuf.FieldDescriptor();
local SHOPRECORDONE_REFRESHCOUNT_FIELD = protobuf.FieldDescriptor();
local SHOPRECORDONE_REFRESHTIME_FIELD = protobuf.FieldDescriptor();
local SHOPRECORDONE_REFRESHDAY_FIELD = protobuf.FieldDescriptor();
local SHOPRECORDONE_ISHINT_FIELD = protobuf.FieldDescriptor();
local SHOPRECORDONE_WEEKBUYCOUNT_FIELD = protobuf.FieldDescriptor();

SHOPRECORDONE_TYPE_FIELD.name = "type"
SHOPRECORDONE_TYPE_FIELD.full_name = ".KKSG.ShopRecordOne.type"
SHOPRECORDONE_TYPE_FIELD.number = 1
SHOPRECORDONE_TYPE_FIELD.index = 0
SHOPRECORDONE_TYPE_FIELD.label = 1
SHOPRECORDONE_TYPE_FIELD.has_default_value = false
SHOPRECORDONE_TYPE_FIELD.default_value = 0
SHOPRECORDONE_TYPE_FIELD.type = 13
SHOPRECORDONE_TYPE_FIELD.cpp_type = 3

SHOPRECORDONE_UPDATETIME_FIELD.name = "updatetime"
SHOPRECORDONE_UPDATETIME_FIELD.full_name = ".KKSG.ShopRecordOne.updatetime"
SHOPRECORDONE_UPDATETIME_FIELD.number = 2
SHOPRECORDONE_UPDATETIME_FIELD.index = 1
SHOPRECORDONE_UPDATETIME_FIELD.label = 1
SHOPRECORDONE_UPDATETIME_FIELD.has_default_value = false
SHOPRECORDONE_UPDATETIME_FIELD.default_value = 0
SHOPRECORDONE_UPDATETIME_FIELD.type = 13
SHOPRECORDONE_UPDATETIME_FIELD.cpp_type = 3

SHOPRECORDONE_ITEMS_FIELD.name = "items"
SHOPRECORDONE_ITEMS_FIELD.full_name = ".KKSG.ShopRecordOne.items"
SHOPRECORDONE_ITEMS_FIELD.number = 3
SHOPRECORDONE_ITEMS_FIELD.index = 2
SHOPRECORDONE_ITEMS_FIELD.label = 3
SHOPRECORDONE_ITEMS_FIELD.has_default_value = false
SHOPRECORDONE_ITEMS_FIELD.default_value = {}
SHOPRECORDONE_ITEMS_FIELD.message_type = ITEM_PB.ITEM
SHOPRECORDONE_ITEMS_FIELD.type = 11
SHOPRECORDONE_ITEMS_FIELD.cpp_type = 10

SHOPRECORDONE_SLOTS_FIELD.name = "slots"
SHOPRECORDONE_SLOTS_FIELD.full_name = ".KKSG.ShopRecordOne.slots"
SHOPRECORDONE_SLOTS_FIELD.number = 4
SHOPRECORDONE_SLOTS_FIELD.index = 3
SHOPRECORDONE_SLOTS_FIELD.label = 3
SHOPRECORDONE_SLOTS_FIELD.has_default_value = false
SHOPRECORDONE_SLOTS_FIELD.default_value = {}
SHOPRECORDONE_SLOTS_FIELD.type = 13
SHOPRECORDONE_SLOTS_FIELD.cpp_type = 3

SHOPRECORDONE_BUYCOUNT_FIELD.name = "buycount"
SHOPRECORDONE_BUYCOUNT_FIELD.full_name = ".KKSG.ShopRecordOne.buycount"
SHOPRECORDONE_BUYCOUNT_FIELD.number = 5
SHOPRECORDONE_BUYCOUNT_FIELD.index = 4
SHOPRECORDONE_BUYCOUNT_FIELD.label = 3
SHOPRECORDONE_BUYCOUNT_FIELD.has_default_value = false
SHOPRECORDONE_BUYCOUNT_FIELD.default_value = {}
SHOPRECORDONE_BUYCOUNT_FIELD.message_type = ITEMBRIEF_PB.ITEMBRIEF
SHOPRECORDONE_BUYCOUNT_FIELD.type = 11
SHOPRECORDONE_BUYCOUNT_FIELD.cpp_type = 10

SHOPRECORDONE_DAILYBUYCOUNT_FIELD.name = "dailybuycount"
SHOPRECORDONE_DAILYBUYCOUNT_FIELD.full_name = ".KKSG.ShopRecordOne.dailybuycount"
SHOPRECORDONE_DAILYBUYCOUNT_FIELD.number = 6
SHOPRECORDONE_DAILYBUYCOUNT_FIELD.index = 5
SHOPRECORDONE_DAILYBUYCOUNT_FIELD.label = 3
SHOPRECORDONE_DAILYBUYCOUNT_FIELD.has_default_value = false
SHOPRECORDONE_DAILYBUYCOUNT_FIELD.default_value = {}
SHOPRECORDONE_DAILYBUYCOUNT_FIELD.message_type = ITEMBRIEF_PB.ITEMBRIEF
SHOPRECORDONE_DAILYBUYCOUNT_FIELD.type = 11
SHOPRECORDONE_DAILYBUYCOUNT_FIELD.cpp_type = 10

SHOPRECORDONE_REFRESHCOUNT_FIELD.name = "refreshcount"
SHOPRECORDONE_REFRESHCOUNT_FIELD.full_name = ".KKSG.ShopRecordOne.refreshcount"
SHOPRECORDONE_REFRESHCOUNT_FIELD.number = 7
SHOPRECORDONE_REFRESHCOUNT_FIELD.index = 6
SHOPRECORDONE_REFRESHCOUNT_FIELD.label = 1
SHOPRECORDONE_REFRESHCOUNT_FIELD.has_default_value = false
SHOPRECORDONE_REFRESHCOUNT_FIELD.default_value = 0
SHOPRECORDONE_REFRESHCOUNT_FIELD.type = 13
SHOPRECORDONE_REFRESHCOUNT_FIELD.cpp_type = 3

SHOPRECORDONE_REFRESHTIME_FIELD.name = "refreshtime"
SHOPRECORDONE_REFRESHTIME_FIELD.full_name = ".KKSG.ShopRecordOne.refreshtime"
SHOPRECORDONE_REFRESHTIME_FIELD.number = 8
SHOPRECORDONE_REFRESHTIME_FIELD.index = 7
SHOPRECORDONE_REFRESHTIME_FIELD.label = 1
SHOPRECORDONE_REFRESHTIME_FIELD.has_default_value = false
SHOPRECORDONE_REFRESHTIME_FIELD.default_value = 0
SHOPRECORDONE_REFRESHTIME_FIELD.type = 13
SHOPRECORDONE_REFRESHTIME_FIELD.cpp_type = 3

SHOPRECORDONE_REFRESHDAY_FIELD.name = "refreshday"
SHOPRECORDONE_REFRESHDAY_FIELD.full_name = ".KKSG.ShopRecordOne.refreshday"
SHOPRECORDONE_REFRESHDAY_FIELD.number = 9
SHOPRECORDONE_REFRESHDAY_FIELD.index = 8
SHOPRECORDONE_REFRESHDAY_FIELD.label = 1
SHOPRECORDONE_REFRESHDAY_FIELD.has_default_value = false
SHOPRECORDONE_REFRESHDAY_FIELD.default_value = 0
SHOPRECORDONE_REFRESHDAY_FIELD.type = 13
SHOPRECORDONE_REFRESHDAY_FIELD.cpp_type = 3

SHOPRECORDONE_ISHINT_FIELD.name = "ishint"
SHOPRECORDONE_ISHINT_FIELD.full_name = ".KKSG.ShopRecordOne.ishint"
SHOPRECORDONE_ISHINT_FIELD.number = 10
SHOPRECORDONE_ISHINT_FIELD.index = 9
SHOPRECORDONE_ISHINT_FIELD.label = 1
SHOPRECORDONE_ISHINT_FIELD.has_default_value = false
SHOPRECORDONE_ISHINT_FIELD.default_value = false
SHOPRECORDONE_ISHINT_FIELD.type = 8
SHOPRECORDONE_ISHINT_FIELD.cpp_type = 7

SHOPRECORDONE_WEEKBUYCOUNT_FIELD.name = "weekbuycount"
SHOPRECORDONE_WEEKBUYCOUNT_FIELD.full_name = ".KKSG.ShopRecordOne.weekbuycount"
SHOPRECORDONE_WEEKBUYCOUNT_FIELD.number = 11
SHOPRECORDONE_WEEKBUYCOUNT_FIELD.index = 10
SHOPRECORDONE_WEEKBUYCOUNT_FIELD.label = 3
SHOPRECORDONE_WEEKBUYCOUNT_FIELD.has_default_value = false
SHOPRECORDONE_WEEKBUYCOUNT_FIELD.default_value = {}
SHOPRECORDONE_WEEKBUYCOUNT_FIELD.message_type = ITEMBRIEF_PB.ITEMBRIEF
SHOPRECORDONE_WEEKBUYCOUNT_FIELD.type = 11
SHOPRECORDONE_WEEKBUYCOUNT_FIELD.cpp_type = 10

SHOPRECORDONE.name = "ShopRecordOne"
SHOPRECORDONE.full_name = ".KKSG.ShopRecordOne"
SHOPRECORDONE.nested_types = {}
SHOPRECORDONE.enum_types = {}
SHOPRECORDONE.fields = {SHOPRECORDONE_TYPE_FIELD, SHOPRECORDONE_UPDATETIME_FIELD, SHOPRECORDONE_ITEMS_FIELD, SHOPRECORDONE_SLOTS_FIELD, SHOPRECORDONE_BUYCOUNT_FIELD, SHOPRECORDONE_DAILYBUYCOUNT_FIELD, SHOPRECORDONE_REFRESHCOUNT_FIELD, SHOPRECORDONE_REFRESHTIME_FIELD, SHOPRECORDONE_REFRESHDAY_FIELD, SHOPRECORDONE_ISHINT_FIELD, SHOPRECORDONE_WEEKBUYCOUNT_FIELD}
SHOPRECORDONE.is_extendable = false
SHOPRECORDONE.extensions = {}

ShopRecordOne = protobuf.Message(SHOPRECORDONE)


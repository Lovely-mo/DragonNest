-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
module('ItemRecord_pb')


ITEMRECORD = protobuf.Descriptor();
local ITEMRECORD_ITEMID_FIELD = protobuf.FieldDescriptor();
local ITEMRECORD_ITEMCOUNT_FIELD = protobuf.FieldDescriptor();
local ITEMRECORD_ISRECEIVE_FIELD = protobuf.FieldDescriptor();

ITEMRECORD_ITEMID_FIELD.name = "itemID"
ITEMRECORD_ITEMID_FIELD.full_name = ".KKSG.ItemRecord.itemID"
ITEMRECORD_ITEMID_FIELD.number = 1
ITEMRECORD_ITEMID_FIELD.index = 0
ITEMRECORD_ITEMID_FIELD.label = 1
ITEMRECORD_ITEMID_FIELD.has_default_value = false
ITEMRECORD_ITEMID_FIELD.default_value = 0
ITEMRECORD_ITEMID_FIELD.type = 13
ITEMRECORD_ITEMID_FIELD.cpp_type = 3

ITEMRECORD_ITEMCOUNT_FIELD.name = "itemCount"
ITEMRECORD_ITEMCOUNT_FIELD.full_name = ".KKSG.ItemRecord.itemCount"
ITEMRECORD_ITEMCOUNT_FIELD.number = 2
ITEMRECORD_ITEMCOUNT_FIELD.index = 1
ITEMRECORD_ITEMCOUNT_FIELD.label = 1
ITEMRECORD_ITEMCOUNT_FIELD.has_default_value = false
ITEMRECORD_ITEMCOUNT_FIELD.default_value = 0
ITEMRECORD_ITEMCOUNT_FIELD.type = 13
ITEMRECORD_ITEMCOUNT_FIELD.cpp_type = 3

ITEMRECORD_ISRECEIVE_FIELD.name = "isreceive"
ITEMRECORD_ISRECEIVE_FIELD.full_name = ".KKSG.ItemRecord.isreceive"
ITEMRECORD_ISRECEIVE_FIELD.number = 3
ITEMRECORD_ISRECEIVE_FIELD.index = 2
ITEMRECORD_ISRECEIVE_FIELD.label = 1
ITEMRECORD_ISRECEIVE_FIELD.has_default_value = false
ITEMRECORD_ISRECEIVE_FIELD.default_value = false
ITEMRECORD_ISRECEIVE_FIELD.type = 8
ITEMRECORD_ISRECEIVE_FIELD.cpp_type = 7

ITEMRECORD.name = "ItemRecord"
ITEMRECORD.full_name = ".KKSG.ItemRecord"
ITEMRECORD.nested_types = {}
ITEMRECORD.enum_types = {}
ITEMRECORD.fields = {ITEMRECORD_ITEMID_FIELD, ITEMRECORD_ITEMCOUNT_FIELD, ITEMRECORD_ISRECEIVE_FIELD}
ITEMRECORD.is_extendable = false
ITEMRECORD.extensions = {}

ItemRecord = protobuf.Message(ITEMRECORD)


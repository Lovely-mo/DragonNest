-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
local ATTRIBUTEINFO_PB = require("AttributeInfo_pb")
module('ItemRandAttr_pb')


ITEMRANDATTR = protobuf.Descriptor();
local ITEMRANDATTR_ATTRS_FIELD = protobuf.FieldDescriptor();

ITEMRANDATTR_ATTRS_FIELD.name = "attrs"
ITEMRANDATTR_ATTRS_FIELD.full_name = ".KKSG.ItemRandAttr.attrs"
ITEMRANDATTR_ATTRS_FIELD.number = 1
ITEMRANDATTR_ATTRS_FIELD.index = 0
ITEMRANDATTR_ATTRS_FIELD.label = 3
ITEMRANDATTR_ATTRS_FIELD.has_default_value = false
ITEMRANDATTR_ATTRS_FIELD.default_value = {}
ITEMRANDATTR_ATTRS_FIELD.message_type = ATTRIBUTEINFO_PB.ATTRIBUTEINFO
ITEMRANDATTR_ATTRS_FIELD.type = 11
ITEMRANDATTR_ATTRS_FIELD.cpp_type = 10

ITEMRANDATTR.name = "ItemRandAttr"
ITEMRANDATTR.full_name = ".KKSG.ItemRandAttr"
ITEMRANDATTR.nested_types = {}
ITEMRANDATTR.enum_types = {}
ITEMRANDATTR.fields = {ITEMRANDATTR_ATTRS_FIELD}
ITEMRANDATTR.is_extendable = false
ITEMRANDATTR.extensions = {}

ItemRandAttr = protobuf.Message(ITEMRANDATTR)


-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
module('PlatNotice_pb')


PLATNOTICE = protobuf.Descriptor();
local PLATNOTICE_TYPE_FIELD = protobuf.FieldDescriptor();
local PLATNOTICE_NOTICEID_FIELD = protobuf.FieldDescriptor();
local PLATNOTICE_ISOPEN_FIELD = protobuf.FieldDescriptor();
local PLATNOTICE_AREAID_FIELD = protobuf.FieldDescriptor();
local PLATNOTICE_PLATID_FIELD = protobuf.FieldDescriptor();
local PLATNOTICE_CONTENT_FIELD = protobuf.FieldDescriptor();
local PLATNOTICE_UPDATETIME_FIELD = protobuf.FieldDescriptor();
local PLATNOTICE_ISNEW_FIELD = protobuf.FieldDescriptor();
local PLATNOTICE_TITLE_FIELD = protobuf.FieldDescriptor();

PLATNOTICE_TYPE_FIELD.name = "type"
PLATNOTICE_TYPE_FIELD.full_name = ".KKSG.PlatNotice.type"
PLATNOTICE_TYPE_FIELD.number = 1
PLATNOTICE_TYPE_FIELD.index = 0
PLATNOTICE_TYPE_FIELD.label = 1
PLATNOTICE_TYPE_FIELD.has_default_value = false
PLATNOTICE_TYPE_FIELD.default_value = 0
PLATNOTICE_TYPE_FIELD.type = 13
PLATNOTICE_TYPE_FIELD.cpp_type = 3

PLATNOTICE_NOTICEID_FIELD.name = "noticeid"
PLATNOTICE_NOTICEID_FIELD.full_name = ".KKSG.PlatNotice.noticeid"
PLATNOTICE_NOTICEID_FIELD.number = 2
PLATNOTICE_NOTICEID_FIELD.index = 1
PLATNOTICE_NOTICEID_FIELD.label = 1
PLATNOTICE_NOTICEID_FIELD.has_default_value = false
PLATNOTICE_NOTICEID_FIELD.default_value = 0
PLATNOTICE_NOTICEID_FIELD.type = 13
PLATNOTICE_NOTICEID_FIELD.cpp_type = 3

PLATNOTICE_ISOPEN_FIELD.name = "isopen"
PLATNOTICE_ISOPEN_FIELD.full_name = ".KKSG.PlatNotice.isopen"
PLATNOTICE_ISOPEN_FIELD.number = 3
PLATNOTICE_ISOPEN_FIELD.index = 2
PLATNOTICE_ISOPEN_FIELD.label = 1
PLATNOTICE_ISOPEN_FIELD.has_default_value = false
PLATNOTICE_ISOPEN_FIELD.default_value = false
PLATNOTICE_ISOPEN_FIELD.type = 8
PLATNOTICE_ISOPEN_FIELD.cpp_type = 7

PLATNOTICE_AREAID_FIELD.name = "areaid"
PLATNOTICE_AREAID_FIELD.full_name = ".KKSG.PlatNotice.areaid"
PLATNOTICE_AREAID_FIELD.number = 4
PLATNOTICE_AREAID_FIELD.index = 3
PLATNOTICE_AREAID_FIELD.label = 1
PLATNOTICE_AREAID_FIELD.has_default_value = false
PLATNOTICE_AREAID_FIELD.default_value = 0
PLATNOTICE_AREAID_FIELD.type = 13
PLATNOTICE_AREAID_FIELD.cpp_type = 3

PLATNOTICE_PLATID_FIELD.name = "platid"
PLATNOTICE_PLATID_FIELD.full_name = ".KKSG.PlatNotice.platid"
PLATNOTICE_PLATID_FIELD.number = 5
PLATNOTICE_PLATID_FIELD.index = 4
PLATNOTICE_PLATID_FIELD.label = 1
PLATNOTICE_PLATID_FIELD.has_default_value = false
PLATNOTICE_PLATID_FIELD.default_value = 0
PLATNOTICE_PLATID_FIELD.type = 13
PLATNOTICE_PLATID_FIELD.cpp_type = 3

PLATNOTICE_CONTENT_FIELD.name = "content"
PLATNOTICE_CONTENT_FIELD.full_name = ".KKSG.PlatNotice.content"
PLATNOTICE_CONTENT_FIELD.number = 6
PLATNOTICE_CONTENT_FIELD.index = 5
PLATNOTICE_CONTENT_FIELD.label = 1
PLATNOTICE_CONTENT_FIELD.has_default_value = false
PLATNOTICE_CONTENT_FIELD.default_value = ""
PLATNOTICE_CONTENT_FIELD.type = 9
PLATNOTICE_CONTENT_FIELD.cpp_type = 9

PLATNOTICE_UPDATETIME_FIELD.name = "updatetime"
PLATNOTICE_UPDATETIME_FIELD.full_name = ".KKSG.PlatNotice.updatetime"
PLATNOTICE_UPDATETIME_FIELD.number = 7
PLATNOTICE_UPDATETIME_FIELD.index = 6
PLATNOTICE_UPDATETIME_FIELD.label = 1
PLATNOTICE_UPDATETIME_FIELD.has_default_value = false
PLATNOTICE_UPDATETIME_FIELD.default_value = 0
PLATNOTICE_UPDATETIME_FIELD.type = 13
PLATNOTICE_UPDATETIME_FIELD.cpp_type = 3

PLATNOTICE_ISNEW_FIELD.name = "isnew"
PLATNOTICE_ISNEW_FIELD.full_name = ".KKSG.PlatNotice.isnew"
PLATNOTICE_ISNEW_FIELD.number = 8
PLATNOTICE_ISNEW_FIELD.index = 7
PLATNOTICE_ISNEW_FIELD.label = 1
PLATNOTICE_ISNEW_FIELD.has_default_value = false
PLATNOTICE_ISNEW_FIELD.default_value = false
PLATNOTICE_ISNEW_FIELD.type = 8
PLATNOTICE_ISNEW_FIELD.cpp_type = 7

PLATNOTICE_TITLE_FIELD.name = "title"
PLATNOTICE_TITLE_FIELD.full_name = ".KKSG.PlatNotice.title"
PLATNOTICE_TITLE_FIELD.number = 9
PLATNOTICE_TITLE_FIELD.index = 8
PLATNOTICE_TITLE_FIELD.label = 1
PLATNOTICE_TITLE_FIELD.has_default_value = false
PLATNOTICE_TITLE_FIELD.default_value = ""
PLATNOTICE_TITLE_FIELD.type = 9
PLATNOTICE_TITLE_FIELD.cpp_type = 9

PLATNOTICE.name = "PlatNotice"
PLATNOTICE.full_name = ".KKSG.PlatNotice"
PLATNOTICE.nested_types = {}
PLATNOTICE.enum_types = {}
PLATNOTICE.fields = {PLATNOTICE_TYPE_FIELD, PLATNOTICE_NOTICEID_FIELD, PLATNOTICE_ISOPEN_FIELD, PLATNOTICE_AREAID_FIELD, PLATNOTICE_PLATID_FIELD, PLATNOTICE_CONTENT_FIELD, PLATNOTICE_UPDATETIME_FIELD, PLATNOTICE_ISNEW_FIELD, PLATNOTICE_TITLE_FIELD}
PLATNOTICE.is_extendable = false
PLATNOTICE.extensions = {}

PlatNotice = protobuf.Message(PLATNOTICE)


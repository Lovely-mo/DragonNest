-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf/protobuf"
module('PkBaseHist_pb')


PKBASEHIST = protobuf.Descriptor();
local PKBASEHIST_WIN_FIELD = protobuf.FieldDescriptor();
local PKBASEHIST_LOSE_FIELD = protobuf.FieldDescriptor();
local PKBASEHIST_DRAW_FIELD = protobuf.FieldDescriptor();
local PKBASEHIST_LASTWIN_FIELD = protobuf.FieldDescriptor();
local PKBASEHIST_LASTLOSE_FIELD = protobuf.FieldDescriptor();
local PKBASEHIST_CONTINUEWIN_FIELD = protobuf.FieldDescriptor();
local PKBASEHIST_CONTINUELOSE_FIELD = protobuf.FieldDescriptor();

PKBASEHIST_WIN_FIELD.name = "win"
PKBASEHIST_WIN_FIELD.full_name = ".KKSG.PkBaseHist.win"
PKBASEHIST_WIN_FIELD.number = 1
PKBASEHIST_WIN_FIELD.index = 0
PKBASEHIST_WIN_FIELD.label = 1
PKBASEHIST_WIN_FIELD.has_default_value = false
PKBASEHIST_WIN_FIELD.default_value = 0
PKBASEHIST_WIN_FIELD.type = 13
PKBASEHIST_WIN_FIELD.cpp_type = 3

PKBASEHIST_LOSE_FIELD.name = "lose"
PKBASEHIST_LOSE_FIELD.full_name = ".KKSG.PkBaseHist.lose"
PKBASEHIST_LOSE_FIELD.number = 2
PKBASEHIST_LOSE_FIELD.index = 1
PKBASEHIST_LOSE_FIELD.label = 1
PKBASEHIST_LOSE_FIELD.has_default_value = false
PKBASEHIST_LOSE_FIELD.default_value = 0
PKBASEHIST_LOSE_FIELD.type = 13
PKBASEHIST_LOSE_FIELD.cpp_type = 3

PKBASEHIST_DRAW_FIELD.name = "draw"
PKBASEHIST_DRAW_FIELD.full_name = ".KKSG.PkBaseHist.draw"
PKBASEHIST_DRAW_FIELD.number = 3
PKBASEHIST_DRAW_FIELD.index = 2
PKBASEHIST_DRAW_FIELD.label = 1
PKBASEHIST_DRAW_FIELD.has_default_value = false
PKBASEHIST_DRAW_FIELD.default_value = 0
PKBASEHIST_DRAW_FIELD.type = 13
PKBASEHIST_DRAW_FIELD.cpp_type = 3

PKBASEHIST_LASTWIN_FIELD.name = "lastwin"
PKBASEHIST_LASTWIN_FIELD.full_name = ".KKSG.PkBaseHist.lastwin"
PKBASEHIST_LASTWIN_FIELD.number = 4
PKBASEHIST_LASTWIN_FIELD.index = 3
PKBASEHIST_LASTWIN_FIELD.label = 1
PKBASEHIST_LASTWIN_FIELD.has_default_value = false
PKBASEHIST_LASTWIN_FIELD.default_value = 0
PKBASEHIST_LASTWIN_FIELD.type = 13
PKBASEHIST_LASTWIN_FIELD.cpp_type = 3

PKBASEHIST_LASTLOSE_FIELD.name = "lastlose"
PKBASEHIST_LASTLOSE_FIELD.full_name = ".KKSG.PkBaseHist.lastlose"
PKBASEHIST_LASTLOSE_FIELD.number = 5
PKBASEHIST_LASTLOSE_FIELD.index = 4
PKBASEHIST_LASTLOSE_FIELD.label = 1
PKBASEHIST_LASTLOSE_FIELD.has_default_value = false
PKBASEHIST_LASTLOSE_FIELD.default_value = 0
PKBASEHIST_LASTLOSE_FIELD.type = 13
PKBASEHIST_LASTLOSE_FIELD.cpp_type = 3

PKBASEHIST_CONTINUEWIN_FIELD.name = "continuewin"
PKBASEHIST_CONTINUEWIN_FIELD.full_name = ".KKSG.PkBaseHist.continuewin"
PKBASEHIST_CONTINUEWIN_FIELD.number = 6
PKBASEHIST_CONTINUEWIN_FIELD.index = 5
PKBASEHIST_CONTINUEWIN_FIELD.label = 1
PKBASEHIST_CONTINUEWIN_FIELD.has_default_value = false
PKBASEHIST_CONTINUEWIN_FIELD.default_value = 0
PKBASEHIST_CONTINUEWIN_FIELD.type = 13
PKBASEHIST_CONTINUEWIN_FIELD.cpp_type = 3

PKBASEHIST_CONTINUELOSE_FIELD.name = "continuelose"
PKBASEHIST_CONTINUELOSE_FIELD.full_name = ".KKSG.PkBaseHist.continuelose"
PKBASEHIST_CONTINUELOSE_FIELD.number = 7
PKBASEHIST_CONTINUELOSE_FIELD.index = 6
PKBASEHIST_CONTINUELOSE_FIELD.label = 1
PKBASEHIST_CONTINUELOSE_FIELD.has_default_value = false
PKBASEHIST_CONTINUELOSE_FIELD.default_value = 0
PKBASEHIST_CONTINUELOSE_FIELD.type = 13
PKBASEHIST_CONTINUELOSE_FIELD.cpp_type = 3

PKBASEHIST.name = "PkBaseHist"
PKBASEHIST.full_name = ".KKSG.PkBaseHist"
PKBASEHIST.nested_types = {}
PKBASEHIST.enum_types = {}
PKBASEHIST.fields = {PKBASEHIST_WIN_FIELD, PKBASEHIST_LOSE_FIELD, PKBASEHIST_DRAW_FIELD, PKBASEHIST_LASTWIN_FIELD, PKBASEHIST_LASTLOSE_FIELD, PKBASEHIST_CONTINUEWIN_FIELD, PKBASEHIST_CONTINUELOSE_FIELD}
PKBASEHIST.is_extendable = false
PKBASEHIST.extensions = {}

PkBaseHist = protobuf.Message(PKBASEHIST)


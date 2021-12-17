--
--------------------------------------------------------------------------------
--  FILE:  type_checkers.lua
--  DESCRIPTION:  protoc-gen-lua
--      Google's Protocol Buffers project, ported to lua.
--      https://code.google.com/p/protoc-gen-lua/
--
--      Copyright (c) 2010 , 林卓毅 (Zhuoyi Lin) netsnail@gmail.com
--      All rights reserved.
--
--      Use, modification and distribution are subject to the "New BSD License"
--      as listed at <url: http://www.opensource.org/licenses/bsd-license.php >.
--
--  COMPANY:  NetEase
--  CREATED:  2010年07月29日 19时30分37秒 CST
--------------------------------------------------------------------------------
--

local type = type
local error = error
local string = string

module "protobuf.type_checkers"
function TypeChecker(acceptable_types)
    local acceptable_types = acceptable_types

    return function(proposed_value)
        local t = type(proposed_value)
        if acceptable_types[type(proposed_value)] == nil then
            error(string.format('%s has type %s, but expected one of: %s',
                proposed_value, type(proposed_value), acceptable_types))
        end
    end
end

function Int32ValueChecker()
    local _MIN = -2147483648
    local _MAX = 2147483647
    return function(proposed_value)
        if type(proposed_value) ~= 'number' then
            error(string.format('%s has type %s, but expected one of: number',
            proposed_value, type(proposed_value)))
        end
        if _MIN > proposed_value or proposed_value > _MAX then
            error('Value out of range: ' .. proposed_value)
        end
    end
end

function Uint32ValueChecker(IntValueChecker)
    local _MIN = 0
    local _MAX = 0xffffffff

    return function(proposed_value)
        if type(proposed_value) ~= 'number' then
            error(string.format('%s has type %s, but expected one of: number',
                proposed_value, type(proposed_value)))
        end
        if _MIN > proposed_value or proposed_value > _MAX then
            error('Value out of range: ' .. proposed_value)
        end
    end
end

function Int64ValueChecker()
    local _MIN = -9007199254740991
    local _MAX = 9007199254740991

    local _NEG_CODE = 45 -- "-"
    local _ZERO_CODE = 48 -- "0"
    local _NINE_CODE = 57 -- "9"

    return function (proposed_value)

        if type(proposed_value) == 'number' then
            if proposed_value < _MIN or _MAX < proposed_value then
                error('Value out of range64: ' .. proposed_value)
            end
        elseif type(proposed_value) == 'string' then
            local len = string.len(proposed_value)
            local idx = 1
            if len > 0 and string.byte(proposed_value,1) == _NEG_CODE then
                idx = 2
            end
            for i = idx,len do
                local code = string.byte(proposed_value,i)
                if code < _ZERO_CODE or _NINE_CODE < code then
                    error('int64 error fomat: ' .. proposed_value)
                end
            end
        else
            error(string.format('int64 %s has type %s, but expected one of: string or number',
                proposed_value, type(proposed_value)))
        end
    end
end

function UnicodeValueChecker()
    return function (proposed_value)
        if type(proposed_value) ~= 'string' then
            error(string.format('%s has type %s, but expected one of: string', proposed_value, type(proposed_value)))
        end
    end
end

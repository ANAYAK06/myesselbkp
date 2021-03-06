

if (typeof (dojo) != "undefined") {
    dojo.provide("MochiKit.Base");
}
if (typeof (MochiKit) == "undefined") {
    MochiKit = {};
}
if (typeof (MochiKit.Base) == "undefined") {
    MochiKit.Base = {};
}
if (typeof (MochiKit.__export__) == "undefined") {
    MochiKit.__export__ = (MochiKit.__compat__ || (typeof (JSAN) == "undefined" && typeof (dojo) == "undefined"));
}
MochiKit.Base.VERSION = "1.4.2";
MochiKit.Base.NAME = "MochiKit.Base";
MochiKit.Base.update = function(_1, _2) {
    if (_1 === null || _1 === undefined) {
        _1 = {};
    }
    for (var i = 1; i < arguments.length; i++) {
        var o = arguments[i];
        if (typeof (o) != "undefined" && o !== null) {
            for (var k in o) {
                _1[k] = o[k];
            }
        }
    }
    return _1;
};
MochiKit.Base.update(MochiKit.Base, { __repr__: function() {
    return "[" + this.NAME + " " + this.VERSION + "]";
}, toString: function() {
    return this.__repr__();
}, camelize: function(_6) {
    var _7 = _6.split("-");
    var cc = _7[0];
    for (var i = 1; i < _7.length; i++) {
        cc += _7[i].charAt(0).toUpperCase() + _7[i].substring(1);
    }
    return cc;
}, counter: function(n) {
    if (arguments.length === 0) {
        n = 1;
    }
    return function() {
        return n++;
    };
}, clone: function(_b) {
    var me = arguments.callee;
    if (arguments.length == 1) {
        me.prototype = _b;
        return new me();
    }
}, _deps: function(_d, _e) {
    if (!(_d in MochiKit)) {
        MochiKit[_d] = {};
    }
    if (typeof (dojo) != "undefined") {
        dojo.provide("MochiKit." + _d);
    }
    for (var i = 0; i < _e.length; i++) {
        if (typeof (dojo) != "undefined") {
            dojo.require("MochiKit." + _e[i]);
        }
        if (typeof (JSAN) != "undefined") {
            JSAN.use("MochiKit." + _e[i], []);
        }
        if (!(_e[i] in MochiKit)) {
            throw "MochiKit." + _d + " depends on MochiKit." + _e[i] + "!";
        }
    }
}, _flattenArray: function(res, lst) {
    for (var i = 0; i < lst.length; i++) {
        var o = lst[i];
        if (o instanceof Array) {
            arguments.callee(res, o);
        } else {
            res.push(o);
        }
    }
    return res;
}, flattenArray: function(lst) {
    return MochiKit.Base._flattenArray([], lst);
}, flattenArguments: function(lst) {
    var res = [];
    var m = MochiKit.Base;
    var _18 = m.extend(null, arguments);
    while (_18.length) {
        var o = _18.shift();
        if (o && typeof (o) == "object" && typeof (o.length) == "number") {
            for (var i = o.length - 1; i >= 0; i--) {
                _18.unshift(o[i]);
            }
        } else {
            res.push(o);
        }
    }
    return res;
}, extend: function(_1b, obj, _1d) {
    if (!_1d) {
        _1d = 0;
    }
    if (obj) {
        var l = obj.length;
        if (typeof (l) != "number") {
            if (typeof (MochiKit.Iter) != "undefined") {
                obj = MochiKit.Iter.list(obj);
                l = obj.length;
            } else {
                throw new TypeError("Argument not an array-like and MochiKit.Iter not present");
            }
        }
        if (!_1b) {
            _1b = [];
        }
        for (var i = _1d; i < l; i++) {
            _1b.push(obj[i]);
        }
    }
    return _1b;
}, updatetree: function(_20, obj) {
    if (_20 === null || _20 === undefined) {
        _20 = {};
    }
    for (var i = 1; i < arguments.length; i++) {
        var o = arguments[i];
        if (typeof (o) != "undefined" && o !== null) {
            for (var k in o) {
                var v = o[k];
                if (typeof (_20[k]) == "object" && typeof (v) == "object") {
                    arguments.callee(_20[k], v);
                } else {
                    _20[k] = v;
                }
            }
        }
    }
    return _20;
}, setdefault: function(_26, obj) {
    if (_26 === null || _26 === undefined) {
        _26 = {};
    }
    for (var i = 1; i < arguments.length; i++) {
        var o = arguments[i];
        for (var k in o) {
            if (!(k in _26)) {
                _26[k] = o[k];
            }
        }
    }
    return _26;
}, keys: function(obj) {
    var _2c = [];
    for (var _2d in obj) {
        _2c.push(_2d);
    }
    return _2c;
}, values: function(obj) {
    var _2f = [];
    for (var _30 in obj) {
        _2f.push(obj[_30]);
    }
    return _2f;
}, items: function(obj) {
    var _32 = [];
    var e;
    for (var _34 in obj) {
        var v;
        try {
            v = obj[_34];
        }
        catch (e) {
            continue;
        }
        _32.push([_34, v]);
    }
    return _32;
}, _newNamedError: function(_36, _37, _38) {
    _38.prototype = new MochiKit.Base.NamedError(_36.NAME + "." + _37);
    _36[_37] = _38;
}, operator: { truth: function(a) {
    return !!a;
}, lognot: function(a) {
    return !a;
}, identity: function(a) {
    return a;
}, not: function(a) {
    return ~a;
}, neg: function(a) {
    return -a;
}, add: function(a, b) {
    return a + b;
}, sub: function(a, b) {
    return a - b;
}, div: function(a, b) {
    return a / b;
}, mod: function(a, b) {
    return a % b;
}, mul: function(a, b) {
    return a * b;
}, and: function(a, b) {
    return a & b;
}, or: function(a, b) {
    return a | b;
}, xor: function(a, b) {
    return a ^ b;
}, lshift: function(a, b) {
    return a << b;
}, rshift: function(a, b) {
    return a >> b;
}, zrshift: function(a, b) {
    return a >>> b;
}, eq: function(a, b) {
    return a == b;
}, ne: function(a, b) {
    return a != b;
}, gt: function(a, b) {
    return a > b;
}, ge: function(a, b) {
    return a >= b;
}, lt: function(a, b) {
    return a < b;
}, le: function(a, b) {
    return a <= b;
}, seq: function(a, b) {
    return a === b;
}, sne: function(a, b) {
    return a !== b;
}, ceq: function(a, b) {
    return MochiKit.Base.compare(a, b) === 0;
}, cne: function(a, b) {
    return MochiKit.Base.compare(a, b) !== 0;
}, cgt: function(a, b) {
    return MochiKit.Base.compare(a, b) == 1;
}, cge: function(a, b) {
    return MochiKit.Base.compare(a, b) != -1;
}, clt: function(a, b) {
    return MochiKit.Base.compare(a, b) == -1;
}, cle: function(a, b) {
    return MochiKit.Base.compare(a, b) != 1;
}, logand: function(a, b) {
    return a && b;
}, logor: function(a, b) {
    return a || b;
}, contains: function(a, b) {
    return b in a;
} 
}, forwardCall: function(_76) {
    return function() {
        return this[_76].apply(this, arguments);
    };
}, itemgetter: function(_77) {
    return function(arg) {
        return arg[_77];
    };
}, typeMatcher: function() {
    var _79 = {};
    for (var i = 0; i < arguments.length; i++) {
        var typ = arguments[i];
        _79[typ] = typ;
    }
    return function() {
        for (var i = 0; i < arguments.length; i++) {
            if (!(typeof (arguments[i]) in _79)) {
                return false;
            }
        }
        return true;
    };
}, isNull: function() {
    for (var i = 0; i < arguments.length; i++) {
        if (arguments[i] !== null) {
            return false;
        }
    }
    return true;
}, isUndefinedOrNull: function() {
    for (var i = 0; i < arguments.length; i++) {
        var o = arguments[i];
        if (!(typeof (o) == "undefined" || o === null)) {
            return false;
        }
    }
    return true;
}, isEmpty: function(obj) {
    return !MochiKit.Base.isNotEmpty.apply(this, arguments);
}, isNotEmpty: function(obj) {
    for (var i = 0; i < arguments.length; i++) {
        var o = arguments[i];
        if (!(o && o.length)) {
            return false;
        }
    }
    return true;
}, isArrayLike: function() {
    for (var i = 0; i < arguments.length; i++) {
        var o = arguments[i];
        var typ = typeof (o);
        if ((typ != "object" && !(typ == "function" && typeof (o.item) == "function")) || o === null || typeof (o.length) != "number" || o.nodeType === 3 || o.nodeType === 4) {
            return false;
        }
    }
    return true;
}, isDateLike: function() {
    for (var i = 0; i < arguments.length; i++) {
        var o = arguments[i];
        if (typeof (o) != "object" || o === null || typeof (o.getTime) != "function") {
            return false;
        }
    }
    return true;
}, xmap: function(fn) {
    if (fn === null) {
        return MochiKit.Base.extend(null, arguments, 1);
    }
    var _8a = [];
    for (var i = 1; i < arguments.length; i++) {
        _8a.push(fn(arguments[i]));
    }
    return _8a;
}, map: function(fn, lst) {
    var m = MochiKit.Base;
    var itr = MochiKit.Iter;
    var _90 = m.isArrayLike;
    if (arguments.length <= 2) {
        if (!_90(lst)) {
            if (itr) {
                lst = itr.list(lst);
                if (fn === null) {
                    return lst;
                }
            } else {
                throw new TypeError("Argument not an array-like and MochiKit.Iter not present");
            }
        }
        if (fn === null) {
            return m.extend(null, lst);
        }
        var _91 = [];
        for (var i = 0; i < lst.length; i++) {
            _91.push(fn(lst[i]));
        }
        return _91;
    } else {
        if (fn === null) {
            fn = Array;
        }
        var _93 = null;
        for (i = 1; i < arguments.length; i++) {
            if (!_90(arguments[i])) {
                if (itr) {
                    return itr.list(itr.imap.apply(null, arguments));
                } else {
                    throw new TypeError("Argument not an array-like and MochiKit.Iter not present");
                }
            }
            var l = arguments[i].length;
            if (_93 === null || _93 > l) {
                _93 = l;
            }
        }
        _91 = [];
        for (i = 0; i < _93; i++) {
            var _95 = [];
            for (var j = 1; j < arguments.length; j++) {
                _95.push(arguments[j][i]);
            }
            _91.push(fn.apply(this, _95));
        }
        return _91;
    }
}, xfilter: function(fn) {
    var _98 = [];
    if (fn === null) {
        fn = MochiKit.Base.operator.truth;
    }
    for (var i = 1; i < arguments.length; i++) {
        var o = arguments[i];
        if (fn(o)) {
            _98.push(o);
        }
    }
    return _98;
}, filter: function(fn, lst, _9d) {
    var _9e = [];
    var m = MochiKit.Base;
    if (!m.isArrayLike(lst)) {
        if (MochiKit.Iter) {
            lst = MochiKit.Iter.list(lst);
        } else {
            throw new TypeError("Argument not an array-like and MochiKit.Iter not present");
        }
    }
    if (fn === null) {
        fn = m.operator.truth;
    }
    if (typeof (Array.prototype.filter) == "function") {
        return Array.prototype.filter.call(lst, fn, _9d);
    } else {
        if (typeof (_9d) == "undefined" || _9d === null) {
            for (var i = 0; i < lst.length; i++) {
                var o = lst[i];
                if (fn(o)) {
                    _9e.push(o);
                }
            }
        } else {
            for (i = 0; i < lst.length; i++) {
                o = lst[i];
                if (fn.call(_9d, o)) {
                    _9e.push(o);
                }
            }
        }
    }
    return _9e;
}, _wrapDumbFunction: function(_a2) {
    return function() {
        switch (arguments.length) {
            case 0:
                return _a2();
            case 1:
                return _a2(arguments[0]);
            case 2:
                return _a2(arguments[0], arguments[1]);
            case 3:
                return _a2(arguments[0], arguments[1], arguments[2]);
        }
        var _a3 = [];
        for (var i = 0; i < arguments.length; i++) {
            _a3.push("arguments[" + i + "]");
        }
        return eval("(func(" + _a3.join(",") + "))");
    };
}, methodcaller: function(_a5) {
    var _a6 = MochiKit.Base.extend(null, arguments, 1);
    if (typeof (_a5) == "function") {
        return function(obj) {
            return _a5.apply(obj, _a6);
        };
    } else {
        return function(obj) {
            return obj[_a5].apply(obj, _a6);
        };
    }
}, method: function(_a9, _aa) {
    var m = MochiKit.Base;
    return m.bind.apply(this, m.extend([_aa, _a9], arguments, 2));
}, compose: function(f1, f2) {
    var _ae = [];
    var m = MochiKit.Base;
    if (arguments.length === 0) {
        throw new TypeError("compose() requires at least one argument");
    }
    for (var i = 0; i < arguments.length; i++) {
        var fn = arguments[i];
        if (typeof (fn) != "function") {
            throw new TypeError(m.repr(fn) + " is not a function");
        }
        _ae.push(fn);
    }
    return function() {
        var _b2 = arguments;
        for (var i = _ae.length - 1; i >= 0; i--) {
            _b2 = [_ae[i].apply(this, _b2)];
        }
        return _b2[0];
    };
}, bind: function(_b4, _b5) {
    if (typeof (_b4) == "string") {
        _b4 = _b5[_b4];
    }
    var _b6 = _b4.im_func;
    var _b7 = _b4.im_preargs;
    var _b8 = _b4.im_self;
    var m = MochiKit.Base;
    if (typeof (_b4) == "function" && typeof (_b4.apply) == "undefined") {
        _b4 = m._wrapDumbFunction(_b4);
    }
    if (typeof (_b6) != "function") {
        _b6 = _b4;
    }
    if (typeof (_b5) != "undefined") {
        _b8 = _b5;
    }
    if (typeof (_b7) == "undefined") {
        _b7 = [];
    } else {
        _b7 = _b7.slice();
    }
    m.extend(_b7, arguments, 2);
    var _ba = function() {
        var _bb = arguments;
        var me = arguments.callee;
        if (me.im_preargs.length > 0) {
            _bb = m.concat(me.im_preargs, _bb);
        }
        var _bd = me.im_self;
        if (!_bd) {
            _bd = this;
        }
        return me.im_func.apply(_bd, _bb);
    };
    _ba.im_self = _b8;
    _ba.im_func = _b6;
    _ba.im_preargs = _b7;
    return _ba;
}, bindLate: function(_be, _bf) {
    var m = MochiKit.Base;
    if (typeof (_be) != "string") {
        return m.bind.apply(this, arguments);
    }
    var _c1 = m.extend([], arguments, 2);
    var _c2 = function() {
        var _c3 = arguments;
        var me = arguments.callee;
        if (me.im_preargs.length > 0) {
            _c3 = m.concat(me.im_preargs, _c3);
        }
        var _c5 = me.im_self;
        if (!_c5) {
            _c5 = this;
        }
        return _c5[me.im_func].apply(_c5, _c3);
    };
    _c2.im_self = _bf;
    _c2.im_func = _be;
    _c2.im_preargs = _c1;
    return _c2;
}, bindMethods: function(_c6) {
    var _c7 = MochiKit.Base.bind;
    for (var k in _c6) {
        var _c9 = _c6[k];
        if (typeof (_c9) == "function") {
            _c6[k] = _c7(_c9, _c6);
        }
    }
}, registerComparator: function(_ca, _cb, _cc, _cd) {
    MochiKit.Base.comparatorRegistry.register(_ca, _cb, _cc, _cd);
}, _primitives: { "boolean": true, "string": true, "number": true }, compare: function(a, b) {
    if (a == b) {
        return 0;
    }
    var _d0 = (typeof (a) == "undefined" || a === null);
    var _d1 = (typeof (b) == "undefined" || b === null);
    if (_d0 && _d1) {
        return 0;
    } else {
        if (_d0) {
            return -1;
        } else {
            if (_d1) {
                return 1;
            }
        }
    }
    var m = MochiKit.Base;
    var _d3 = m._primitives;
    if (!(typeof (a) in _d3 && typeof (b) in _d3)) {
        try {
            return m.comparatorRegistry.match(a, b);
        }
        catch (e) {
            if (e != m.NotFound) {
                throw e;
            }
        }
    }
    if (a < b) {
        return -1;
    } else {
        if (a > b) {
            return 1;
        }
    }
    var _d4 = m.repr;
    throw new TypeError(_d4(a) + " and " + _d4(b) + " can not be compared");
}, compareDateLike: function(a, b) {
    return MochiKit.Base.compare(a.getTime(), b.getTime());
}, compareArrayLike: function(a, b) {
    var _d9 = MochiKit.Base.compare;
    var _da = a.length;
    var _db = 0;
    if (_da > b.length) {
        _db = 1;
        _da = b.length;
    } else {
        if (_da < b.length) {
            _db = -1;
        }
    }
    for (var i = 0; i < _da; i++) {
        var cmp = _d9(a[i], b[i]);
        if (cmp) {
            return cmp;
        }
    }
    return _db;
}, registerRepr: function(_de, _df, _e0, _e1) {
    MochiKit.Base.reprRegistry.register(_de, _df, _e0, _e1);
}, repr: function(o) {
    if (typeof (o) == "undefined") {
        return "undefined";
    } else {
        if (o === null) {
            return "null";
        }
    }
    try {
        if (typeof (o.__repr__) == "function") {
            return o.__repr__();
        } else {
            if (typeof (o.repr) == "function" && o.repr != arguments.callee) {
                return o.repr();
            }
        }
        return MochiKit.Base.reprRegistry.match(o);
    }
    catch (e) {
        if (typeof (o.NAME) == "string" && (o.toString == Function.prototype.toString || o.toString == Object.prototype.toString)) {
            return o.NAME;
        }
    }
    try {
        var _e3 = (o + "");
    }
    catch (e) {
        return "[" + typeof (o) + "]";
    }
    if (typeof (o) == "function") {
        _e3 = _e3.replace(/^\s+/, "").replace(/\s+/g, " ");
        _e3 = _e3.replace(/,(\S)/, ", $1");
        var idx = _e3.indexOf("{");
        if (idx != -1) {
            _e3 = _e3.substr(0, idx) + "{...}";
        }
    }
    return _e3;
}, reprArrayLike: function(o) {
    var m = MochiKit.Base;
    return "[" + m.map(m.repr, o).join(", ") + "]";
}, reprString: function(o) {
    return ("\"" + o.replace(/(["\\])/g, "\\$1") + "\"").replace(/[\f]/g, "\\f").replace(/[\b]/g, "\\b").replace(/[\n]/g, "\\n").replace(/[\t]/g, "\\t").replace(/[\v]/g, "\\v").replace(/[\r]/g, "\\r");
}, reprNumber: function(o) {
    return o + "";
}, registerJSON: function(_e9, _ea, _eb, _ec) {
    MochiKit.Base.jsonRegistry.register(_e9, _ea, _eb, _ec);
}, evalJSON: function() {
    return eval("(" + MochiKit.Base._filterJSON(arguments[0]) + ")");
}, _filterJSON: function(s) {
    var m = s.match(/^\s*\/\*(.*)\*\/\s*$/);
    if (m) {
        return m[1];
    }
    return s;
}, serializeJSON: function(o) {
    var _f0 = typeof (o);
    if (_f0 == "number" || _f0 == "boolean") {
        return o + "";
    } else {
        if (o === null) {
            return "null";
        } else {
            if (_f0 == "string") {
                var res = "";
                for (var i = 0; i < o.length; i++) {
                    var c = o.charAt(i);
                    if (c == "\"") {
                        res += "\\\"";
                    } else {
                        if (c == "\\") {
                            res += "\\\\";
                        } else {
                            if (c == "\b") {
                                res += "\\b";
                            } else {
                                if (c == "\f") {
                                    res += "\\f";
                                } else {
                                    if (c == "\n") {
                                        res += "\\n";
                                    } else {
                                        if (c == "\r") {
                                            res += "\\r";
                                        } else {
                                            if (c == "\t") {
                                                res += "\\t";
                                            } else {
                                                if (o.charCodeAt(i) <= 31) {
                                                    var hex = o.charCodeAt(i).toString(16);
                                                    if (hex.length < 2) {
                                                        hex = "0" + hex;
                                                    }
                                                    res += "\\u00" + hex.toUpperCase();
                                                } else {
                                                    res += c;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return "\"" + res + "\"";
            }
        }
    }
    var me = arguments.callee;
    var _f6;
    if (typeof (o.__json__) == "function") {
        _f6 = o.__json__();
        if (o !== _f6) {
            return me(_f6);
        }
    }
    if (typeof (o.json) == "function") {
        _f6 = o.json();
        if (o !== _f6) {
            return me(_f6);
        }
    }
    if (_f0 != "function" && typeof (o.length) == "number") {
        var res = [];
        for (var i = 0; i < o.length; i++) {
            var val = me(o[i]);
            if (typeof (val) != "string") {
                continue;
            }
            res.push(val);
        }
        return "[" + res.join(", ") + "]";
    }
    var m = MochiKit.Base;
    try {
        _f6 = m.jsonRegistry.match(o);
        if (o !== _f6) {
            return me(_f6);
        }
    }
    catch (e) {
        if (e != m.NotFound) {
            throw e;
        }
    }
    if (_f0 == "undefined") {
        throw new TypeError("undefined can not be serialized as JSON");
    }
    if (_f0 == "function") {
        return null;
    }
    res = [];
    for (var k in o) {
        var _fa;
        if (typeof (k) == "number") {
            _fa = "\"" + k + "\"";
        } else {
            if (typeof (k) == "string") {
                _fa = me(k);
            } else {
                continue;
            }
        }
        val = me(o[k]);
        if (typeof (val) != "string") {
            continue;
        }
        res.push(_fa + ":" + val);
    }
    return "{" + res.join(", ") + "}";
}, objEqual: function(a, b) {
    return (MochiKit.Base.compare(a, b) === 0);
}, arrayEqual: function(_fd, arr) {
    if (_fd.length != arr.length) {
        return false;
    }
    return (MochiKit.Base.compare(_fd, arr) === 0);
}, concat: function() {
    var _ff = [];
    var _100 = MochiKit.Base.extend;
    for (var i = 0; i < arguments.length; i++) {
        _100(_ff, arguments[i]);
    }
    return _ff;
}, keyComparator: function(key) {
    var m = MochiKit.Base;
    var _104 = m.compare;
    if (arguments.length == 1) {
        return function(a, b) {
            return _104(a[key], b[key]);
        };
    }
    var _107 = m.extend(null, arguments);
    return function(a, b) {
        var rval = 0;
        for (var i = 0; (rval === 0) && (i < _107.length); i++) {
            var key = _107[i];
            rval = _104(a[key], b[key]);
        }
        return rval;
    };
}, reverseKeyComparator: function(key) {
    var _10e = MochiKit.Base.keyComparator.apply(this, arguments);
    return function(a, b) {
        return _10e(b, a);
    };
}, partial: function(func) {
    var m = MochiKit.Base;
    return m.bind.apply(this, m.extend([func, undefined], arguments, 1));
}, listMinMax: function(_113, lst) {
    if (lst.length === 0) {
        return null;
    }
    var cur = lst[0];
    var _116 = MochiKit.Base.compare;
    for (var i = 1; i < lst.length; i++) {
        var o = lst[i];
        if (_116(o, cur) == _113) {
            cur = o;
        }
    }
    return cur;
}, objMax: function() {
    return MochiKit.Base.listMinMax(1, arguments);
}, objMin: function() {
    return MochiKit.Base.listMinMax(-1, arguments);
}, findIdentical: function(lst, _11a, _11b, end) {
    if (typeof (end) == "undefined" || end === null) {
        end = lst.length;
    }
    if (typeof (_11b) == "undefined" || _11b === null) {
        _11b = 0;
    }
    for (var i = _11b; i < end; i++) {
        if (lst[i] === _11a) {
            return i;
        }
    }
    return -1;
}, mean: function() {
    var sum = 0;
    var m = MochiKit.Base;
    var args = m.extend(null, arguments);
    var _121 = args.length;
    while (args.length) {
        var o = args.shift();
        if (o && typeof (o) == "object" && typeof (o.length) == "number") {
            _121 += o.length - 1;
            for (var i = o.length - 1; i >= 0; i--) {
                sum += o[i];
            }
        } else {
            sum += o;
        }
    }
    if (_121 <= 0) {
        throw new TypeError("mean() requires at least one argument");
    }
    return sum / _121;
}, median: function() {
    var data = MochiKit.Base.flattenArguments(arguments);
    if (data.length === 0) {
        throw new TypeError("median() requires at least one argument");
    }
    data.sort(compare);
    if (data.length % 2 == 0) {
        var _125 = data.length / 2;
        return (data[_125] + data[_125 - 1]) / 2;
    } else {
        return data[(data.length - 1) / 2];
    }
}, findValue: function(lst, _127, _128, end) {
    if (typeof (end) == "undefined" || end === null) {
        end = lst.length;
    }
    if (typeof (_128) == "undefined" || _128 === null) {
        _128 = 0;
    }
    var cmp = MochiKit.Base.compare;
    for (var i = _128; i < end; i++) {
        if (cmp(lst[i], _127) === 0) {
            return i;
        }
    }
    return -1;
}, nodeWalk: function(node, _12d) {
    var _12e = [node];
    var _12f = MochiKit.Base.extend;
    while (_12e.length) {
        var res = _12d(_12e.shift());
        if (res) {
            _12f(_12e, res);
        }
    }
}, nameFunctions: function(_131) {
    var base = _131.NAME;
    if (typeof (base) == "undefined") {
        base = "";
    } else {
        base = base + ".";
    }
    for (var name in _131) {
        var o = _131[name];
        if (typeof (o) == "function" && typeof (o.NAME) == "undefined") {
            try {
                o.NAME = base + name;
            }
            catch (e) {
            }
        }
    }
}, queryString: function(_135, _136) {
    if (typeof (MochiKit.DOM) != "undefined" && arguments.length == 1 && (typeof (_135) == "string" || (typeof (_135.nodeType) != "undefined" && _135.nodeType > 0))) {
        var kv = MochiKit.DOM.formContents(_135);
        _135 = kv[0];
        _136 = kv[1];
    } else {
        if (arguments.length == 1) {
            if (typeof (_135.length) == "number" && _135.length == 2) {
                return arguments.callee(_135[0], _135[1]);
            }
            var o = _135;
            _135 = [];
            _136 = [];
            for (var k in o) {
                var v = o[k];
                if (typeof (v) == "function") {
                    continue;
                } else {
                    if (MochiKit.Base.isArrayLike(v)) {
                        for (var i = 0; i < v.length; i++) {
                            _135.push(k);
                            _136.push(v[i]);
                        }
                    } else {
                        _135.push(k);
                        _136.push(v);
                    }
                }
            }
        }
    }
    var rval = [];
    var len = Math.min(_135.length, _136.length);
    var _13e = MochiKit.Base.urlEncode;
    for (var i = 0; i < len; i++) {
        v = _136[i];
        if (typeof (v) != "undefined" && v !== null) {
            rval.push(_13e(_135[i]) + "=" + _13e(v));
        }
    }
    return rval.join("&");
}, parseQueryString: function(_13f, _140) {
    var qstr = (_13f.charAt(0) == "?") ? _13f.substring(1) : _13f;
    var _142 = qstr.replace(/\+/g, "%20").split(/\&amp\;|\&\#38\;|\&#x26;|\&/);
    var o = {};
    var _144;
    if (typeof (decodeURIComponent) != "undefined") {
        _144 = decodeURIComponent;
    } else {
        _144 = unescape;
    }
    if (_140) {
        for (var i = 0; i < _142.length; i++) {
            var pair = _142[i].split("=");
            var name = _144(pair.shift());
            if (!name) {
                continue;
            }
            var arr = o[name];
            if (!(arr instanceof Array)) {
                arr = [];
                o[name] = arr;
            }
            arr.push(_144(pair.join("=")));
        }
    } else {
        for (i = 0; i < _142.length; i++) {
            pair = _142[i].split("=");
            var name = pair.shift();
            if (!name) {
                continue;
            }
            o[_144(name)] = _144(pair.join("="));
        }
    }
    return o;
} 
});
MochiKit.Base.AdapterRegistry = function() {
    this.pairs = [];
};
MochiKit.Base.AdapterRegistry.prototype = { register: function(name, _14a, wrap, _14c) {
    if (_14c) {
        this.pairs.unshift([name, _14a, wrap]);
    } else {
        this.pairs.push([name, _14a, wrap]);
    }
}, match: function() {
    for (var i = 0; i < this.pairs.length; i++) {
        var pair = this.pairs[i];
        if (pair[1].apply(this, arguments)) {
            return pair[2].apply(this, arguments);
        }
    }
    throw MochiKit.Base.NotFound;
}, unregister: function(name) {
    for (var i = 0; i < this.pairs.length; i++) {
        var pair = this.pairs[i];
        if (pair[0] == name) {
            this.pairs.splice(i, 1);
            return true;
        }
    }
    return false;
} 
};
MochiKit.Base.EXPORT = ["flattenArray", "noop", "camelize", "counter", "clone", "extend", "update", "updatetree", "setdefault", "keys", "values", "items", "NamedError", "operator", "forwardCall", "itemgetter", "typeMatcher", "isCallable", "isUndefined", "isUndefinedOrNull", "isNull", "isEmpty", "isNotEmpty", "isArrayLike", "isDateLike", "xmap", "map", "xfilter", "filter", "methodcaller", "compose", "bind", "bindLate", "bindMethods", "NotFound", "AdapterRegistry", "registerComparator", "compare", "registerRepr", "repr", "objEqual", "arrayEqual", "concat", "keyComparator", "reverseKeyComparator", "partial", "merge", "listMinMax", "listMax", "listMin", "objMax", "objMin", "nodeWalk", "zip", "urlEncode", "queryString", "serializeJSON", "registerJSON", "evalJSON", "parseQueryString", "findValue", "findIdentical", "flattenArguments", "method", "average", "mean", "median"];
MochiKit.Base.EXPORT_OK = ["nameFunctions", "comparatorRegistry", "reprRegistry", "jsonRegistry", "compareDateLike", "compareArrayLike", "reprArrayLike", "reprString", "reprNumber"];
MochiKit.Base._exportSymbols = function(_152, _153) {
    if (!MochiKit.__export__) {
        return;
    }
    var all = _153.EXPORT_TAGS[":all"];
    for (var i = 0; i < all.length; i++) {
        _152[all[i]] = _153[all[i]];
    }
};
MochiKit.Base.__new__ = function() {
    var m = this;
    m.noop = m.operator.identity;
    m.forward = m.forwardCall;
    m.find = m.findValue;
    if (typeof (encodeURIComponent) != "undefined") {
        m.urlEncode = function(_157) {
            return encodeURIComponent(_157).replace(/\'/g, "%27");
        };
    } else {
        m.urlEncode = function(_158) {
            return escape(_158).replace(/\+/g, "%2B").replace(/\"/g, "%22").rval.replace(/\'/g, "%27");
        };
    }
    m.NamedError = function(name) {
        this.message = name;
        this.name = name;
    };
    m.NamedError.prototype = new Error();
    m.update(m.NamedError.prototype, { repr: function() {
        if (this.message && this.message != this.name) {
            return this.name + "(" + m.repr(this.message) + ")";
        } else {
            return this.name + "()";
        }
    }, toString: m.forwardCall("repr")
    });
    m.NotFound = new m.NamedError("MochiKit.Base.NotFound");
    m.listMax = m.partial(m.listMinMax, 1);
    m.listMin = m.partial(m.listMinMax, -1);
    m.isCallable = m.typeMatcher("function");
    m.isUndefined = m.typeMatcher("undefined");
    m.merge = m.partial(m.update, null);
    m.zip = m.partial(m.map, null);
    m.average = m.mean;
    m.comparatorRegistry = new m.AdapterRegistry();
    m.registerComparator("dateLike", m.isDateLike, m.compareDateLike);
    m.registerComparator("arrayLike", m.isArrayLike, m.compareArrayLike);
    m.reprRegistry = new m.AdapterRegistry();
    m.registerRepr("arrayLike", m.isArrayLike, m.reprArrayLike);
    m.registerRepr("string", m.typeMatcher("string"), m.reprString);
    m.registerRepr("numbers", m.typeMatcher("number", "boolean"), m.reprNumber);
    m.jsonRegistry = new m.AdapterRegistry();
    var all = m.concat(m.EXPORT, m.EXPORT_OK);
    m.EXPORT_TAGS = { ":common": m.concat(m.EXPORT_OK), ":all": all };
    m.nameFunctions(this);
};
MochiKit.Base.__new__();
if (MochiKit.__export__) {
    compare = MochiKit.Base.compare;
    compose = MochiKit.Base.compose;
    serializeJSON = MochiKit.Base.serializeJSON;
    mean = MochiKit.Base.mean;
    median = MochiKit.Base.median;
}
MochiKit.Base._exportSymbols(this, MochiKit.Base);
MochiKit.Base._deps("Iter", ["Base"]);
MochiKit.Iter.NAME = "MochiKit.Iter";
MochiKit.Iter.VERSION = "1.4.2";
MochiKit.Base.update(MochiKit.Iter, { __repr__: function() {
    return "[" + this.NAME + " " + this.VERSION + "]";
}, toString: function() {
    return this.__repr__();
}, registerIteratorFactory: function(name, _15c, _15d, _15e) {
    MochiKit.Iter.iteratorRegistry.register(name, _15c, _15d, _15e);
}, isIterable: function(o) {
    return o != null && (typeof (o.next) == "function" || typeof (o.iter) == "function");
}, iter: function(_160, _161) {
    var self = MochiKit.Iter;
    if (arguments.length == 2) {
        return self.takewhile(function(a) {
            return a != _161;
        }, _160);
    }
    if (typeof (_160.next) == "function") {
        return _160;
    } else {
        if (typeof (_160.iter) == "function") {
            return _160.iter();
        }
    }
    try {
        return self.iteratorRegistry.match(_160);
    }
    catch (e) {
        var m = MochiKit.Base;
        if (e == m.NotFound) {
            e = new TypeError(typeof (_160) + ": " + m.repr(_160) + " is not iterable");
        }
        throw e;
    }
}, count: function(n) {
    if (!n) {
        n = 0;
    }
    var m = MochiKit.Base;
    return { repr: function() {
        return "count(" + n + ")";
    }, toString: m.forwardCall("repr"), next: m.counter(n)
    };
}, cycle: function(p) {
    var self = MochiKit.Iter;
    var m = MochiKit.Base;
    var lst = [];
    var _16b = self.iter(p);
    return { repr: function() {
        return "cycle(...)";
    }, toString: m.forwardCall("repr"), next: function() {
        try {
            var rval = _16b.next();
            lst.push(rval);
            return rval;
        }
        catch (e) {
            if (e != self.StopIteration) {
                throw e;
            }
            if (lst.length === 0) {
                this.next = function() {
                    throw self.StopIteration;
                };
            } else {
                var i = -1;
                this.next = function() {
                    i = (i + 1) % lst.length;
                    return lst[i];
                };
            }
            return this.next();
        }
    } 
    };
}, repeat: function(elem, n) {
    var m = MochiKit.Base;
    if (typeof (n) == "undefined") {
        return { repr: function() {
            return "repeat(" + m.repr(elem) + ")";
        }, toString: m.forwardCall("repr"), next: function() {
            return elem;
        } 
        };
    }
    return { repr: function() {
        return "repeat(" + m.repr(elem) + ", " + n + ")";
    }, toString: m.forwardCall("repr"), next: function() {
        if (n <= 0) {
            throw MochiKit.Iter.StopIteration;
        }
        n -= 1;
        return elem;
    } 
    };
}, next: function(_171) {
    return _171.next();
}, izip: function(p, q) {
    var m = MochiKit.Base;
    var self = MochiKit.Iter;
    var next = self.next;
    var _177 = m.map(self.iter, arguments);
    return { repr: function() {
        return "izip(...)";
    }, toString: m.forwardCall("repr"), next: function() {
        return m.map(next, _177);
    } 
    };
}, ifilter: function(pred, seq) {
    var m = MochiKit.Base;
    seq = MochiKit.Iter.iter(seq);
    if (pred === null) {
        pred = m.operator.truth;
    }
    return { repr: function() {
        return "ifilter(...)";
    }, toString: m.forwardCall("repr"), next: function() {
        while (true) {
            var rval = seq.next();
            if (pred(rval)) {
                return rval;
            }
        }
        return undefined;
    } 
    };
}, ifilterfalse: function(pred, seq) {
    var m = MochiKit.Base;
    seq = MochiKit.Iter.iter(seq);
    if (pred === null) {
        pred = m.operator.truth;
    }
    return { repr: function() {
        return "ifilterfalse(...)";
    }, toString: m.forwardCall("repr"), next: function() {
        while (true) {
            var rval = seq.next();
            if (!pred(rval)) {
                return rval;
            }
        }
        return undefined;
    } 
    };
}, islice: function(seq) {
    var self = MochiKit.Iter;
    var m = MochiKit.Base;
    seq = self.iter(seq);
    var _183 = 0;
    var stop = 0;
    var step = 1;
    var i = -1;
    if (arguments.length == 2) {
        stop = arguments[1];
    } else {
        if (arguments.length == 3) {
            _183 = arguments[1];
            stop = arguments[2];
        } else {
            _183 = arguments[1];
            stop = arguments[2];
            step = arguments[3];
        }
    }
    return { repr: function() {
        return "islice(" + ["...", _183, stop, step].join(", ") + ")";
    }, toString: m.forwardCall("repr"), next: function() {
        var rval;
        while (i < _183) {
            rval = seq.next();
            i++;
        }
        if (_183 >= stop) {
            throw self.StopIteration;
        }
        _183 += step;
        return rval;
    } 
    };
}, imap: function(fun, p, q) {
    var m = MochiKit.Base;
    var self = MochiKit.Iter;
    var _18d = m.map(self.iter, m.extend(null, arguments, 1));
    var map = m.map;
    var next = self.next;
    return { repr: function() {
        return "imap(...)";
    }, toString: m.forwardCall("repr"), next: function() {
        return fun.apply(this, map(next, _18d));
    } 
    };
}, applymap: function(fun, seq, self) {
    seq = MochiKit.Iter.iter(seq);
    var m = MochiKit.Base;
    return { repr: function() {
        return "applymap(...)";
    }, toString: m.forwardCall("repr"), next: function() {
        return fun.apply(self, seq.next());
    } 
    };
}, chain: function(p, q) {
    var self = MochiKit.Iter;
    var m = MochiKit.Base;
    if (arguments.length == 1) {
        return self.iter(arguments[0]);
    }
    var _198 = m.map(self.iter, arguments);
    return { repr: function() {
        return "chain(...)";
    }, toString: m.forwardCall("repr"), next: function() {
        while (_198.length > 1) {
            try {
                var _199 = _198[0].next();
                return _199;
            }
            catch (e) {
                if (e != self.StopIteration) {
                    throw e;
                }
                _198.shift();
                var _199 = _198[0].next();
                return _199;
            }
        }
        if (_198.length == 1) {
            var arg = _198.shift();
            this.next = m.bind("next", arg);
            return this.next();
        }
        throw self.StopIteration;
    } 
    };
}, takewhile: function(pred, seq) {
    var self = MochiKit.Iter;
    seq = self.iter(seq);
    return { repr: function() {
        return "takewhile(...)";
    }, toString: MochiKit.Base.forwardCall("repr"), next: function() {
        var rval = seq.next();
        if (!pred(rval)) {
            this.next = function() {
                throw self.StopIteration;
            };
            this.next();
        }
        return rval;
    } 
    };
}, dropwhile: function(pred, seq) {
    seq = MochiKit.Iter.iter(seq);
    var m = MochiKit.Base;
    var bind = m.bind;
    return { "repr": function() {
        return "dropwhile(...)";
    }, "toString": m.forwardCall("repr"), "next": function() {
        while (true) {
            var rval = seq.next();
            if (!pred(rval)) {
                break;
            }
        }
        this.next = bind("next", seq);
        return rval;
    } 
    };
}, _tee: function(_1a4, sync, _1a6) {
    sync.pos[_1a4] = -1;
    var m = MochiKit.Base;
    var _1a8 = m.listMin;
    return { repr: function() {
        return "tee(" + _1a4 + ", ...)";
    }, toString: m.forwardCall("repr"), next: function() {
        var rval;
        var i = sync.pos[_1a4];
        if (i == sync.max) {
            rval = _1a6.next();
            sync.deque.push(rval);
            sync.max += 1;
            sync.pos[_1a4] += 1;
        } else {
            rval = sync.deque[i - sync.min];
            sync.pos[_1a4] += 1;
            if (i == sync.min && _1a8(sync.pos) != sync.min) {
                sync.min += 1;
                sync.deque.shift();
            }
        }
        return rval;
    } 
    };
}, tee: function(_1ab, n) {
    var rval = [];
    var sync = { "pos": [], "deque": [], "max": -1, "min": -1 };
    if (arguments.length == 1 || typeof (n) == "undefined" || n === null) {
        n = 2;
    }
    var self = MochiKit.Iter;
    _1ab = self.iter(_1ab);
    var _tee = self._tee;
    for (var i = 0; i < n; i++) {
        rval.push(_tee(i, sync, _1ab));
    }
    return rval;
}, list: function(_1b2) {
    var rval;
    if (_1b2 instanceof Array) {
        return _1b2.slice();
    }
    if (typeof (_1b2) == "function" && !(_1b2 instanceof Function) && typeof (_1b2.length) == "number") {
        rval = [];
        for (var i = 0; i < _1b2.length; i++) {
            rval.push(_1b2[i]);
        }
        return rval;
    }
    var self = MochiKit.Iter;
    _1b2 = self.iter(_1b2);
    var rval = [];
    var _1b6;
    try {
        while (true) {
            _1b6 = _1b2.next();
            rval.push(_1b6);
        }
    }
    catch (e) {
        if (e != self.StopIteration) {
            throw e;
        }
        return rval;
    }
    return undefined;
}, reduce: function(fn, _1b8, _1b9) {
    var i = 0;
    var x = _1b9;
    var self = MochiKit.Iter;
    _1b8 = self.iter(_1b8);
    if (arguments.length < 3) {
        try {
            x = _1b8.next();
        }
        catch (e) {
            if (e == self.StopIteration) {
                e = new TypeError("reduce() of empty sequence with no initial value");
            }
            throw e;
        }
        i++;
    }
    try {
        while (true) {
            x = fn(x, _1b8.next());
        }
    }
    catch (e) {
        if (e != self.StopIteration) {
            throw e;
        }
    }
    return x;
}, range: function() {
    var _1bd = 0;
    var stop = 0;
    var step = 1;
    if (arguments.length == 1) {
        stop = arguments[0];
    } else {
        if (arguments.length == 2) {
            _1bd = arguments[0];
            stop = arguments[1];
        } else {
            if (arguments.length == 3) {
                _1bd = arguments[0];
                stop = arguments[1];
                step = arguments[2];
            } else {
                throw new TypeError("range() takes 1, 2, or 3 arguments!");
            }
        }
    }
    if (step === 0) {
        throw new TypeError("range() step must not be 0");
    }
    return { next: function() {
        if ((step > 0 && _1bd >= stop) || (step < 0 && _1bd <= stop)) {
            throw MochiKit.Iter.StopIteration;
        }
        var rval = _1bd;
        _1bd += step;
        return rval;
    }, repr: function() {
        return "range(" + [_1bd, stop, step].join(", ") + ")";
    }, toString: MochiKit.Base.forwardCall("repr")
    };
}, sum: function(_1c1, _1c2) {
    if (typeof (_1c2) == "undefined" || _1c2 === null) {
        _1c2 = 0;
    }
    var x = _1c2;
    var self = MochiKit.Iter;
    _1c1 = self.iter(_1c1);
    try {
        while (true) {
            x += _1c1.next();
        }
    }
    catch (e) {
        if (e != self.StopIteration) {
            throw e;
        }
    }
    return x;
}, exhaust: function(_1c5) {
    var self = MochiKit.Iter;
    _1c5 = self.iter(_1c5);
    try {
        while (true) {
            _1c5.next();
        }
    }
    catch (e) {
        if (e != self.StopIteration) {
            throw e;
        }
    }
}, forEach: function(_1c7, func, obj) {
    var m = MochiKit.Base;
    var self = MochiKit.Iter;
    if (arguments.length > 2) {
        func = m.bind(func, obj);
    }
    if (m.isArrayLike(_1c7) && !self.isIterable(_1c7)) {
        try {
            for (var i = 0; i < _1c7.length; i++) {
                func(_1c7[i]);
            }
        }
        catch (e) {
            if (e != self.StopIteration) {
                throw e;
            }
        }
    } else {
        self.exhaust(self.imap(func, _1c7));
    }
}, every: function(_1cd, func) {
    var self = MochiKit.Iter;
    try {
        self.ifilterfalse(func, _1cd).next();
        return false;
    }
    catch (e) {
        if (e != self.StopIteration) {
            throw e;
        }
        return true;
    }
}, sorted: function(_1d0, cmp) {
    var rval = MochiKit.Iter.list(_1d0);
    if (arguments.length == 1) {
        cmp = MochiKit.Base.compare;
    }
    rval.sort(cmp);
    return rval;
}, reversed: function(_1d3) {
    var rval = MochiKit.Iter.list(_1d3);
    rval.reverse();
    return rval;
}, some: function(_1d5, func) {
    var self = MochiKit.Iter;
    try {
        self.ifilter(func, _1d5).next();
        return true;
    }
    catch (e) {
        if (e != self.StopIteration) {
            throw e;
        }
        return false;
    }
}, iextend: function(lst, _1d9) {
    var m = MochiKit.Base;
    var self = MochiKit.Iter;
    if (m.isArrayLike(_1d9) && !self.isIterable(_1d9)) {
        for (var i = 0; i < _1d9.length; i++) {
            lst.push(_1d9[i]);
        }
    } else {
        _1d9 = self.iter(_1d9);
        try {
            while (true) {
                lst.push(_1d9.next());
            }
        }
        catch (e) {
            if (e != self.StopIteration) {
                throw e;
            }
        }
    }
    return lst;
}, groupby: function(_1dd, _1de) {
    var m = MochiKit.Base;
    var self = MochiKit.Iter;
    if (arguments.length < 2) {
        _1de = m.operator.identity;
    }
    _1dd = self.iter(_1dd);
    var pk = undefined;
    var k = undefined;
    var v;
    function fetch() {
        v = _1dd.next();
        k = _1de(v);
    }
    function eat() {
        var ret = v;
        v = undefined;
        return ret;
    }
    var _1e5 = true;
    var _1e6 = m.compare;
    return { repr: function() {
        return "groupby(...)";
    }, next: function() {
        while (_1e6(k, pk) === 0) {
            fetch();
            if (_1e5) {
                _1e5 = false;
                break;
            }
        }
        pk = k;
        return [k, { next: function() {
            if (v == undefined) {
                fetch();
            }
            if (_1e6(k, pk) !== 0) {
                throw self.StopIteration;
            }
            return eat();
        } }];
        } 
    };
}, groupby_as_array: function(_1e7, _1e8) {
    var m = MochiKit.Base;
    var self = MochiKit.Iter;
    if (arguments.length < 2) {
        _1e8 = m.operator.identity;
    }
    _1e7 = self.iter(_1e7);
    var _1eb = [];
    var _1ec = true;
    var _1ed;
    var _1ee = m.compare;
    while (true) {
        try {
            var _1ef = _1e7.next();
            var key = _1e8(_1ef);
        }
        catch (e) {
            if (e == self.StopIteration) {
                break;
            }
            throw e;
        }
        if (_1ec || _1ee(key, _1ed) !== 0) {
            var _1f1 = [];
            _1eb.push([key, _1f1]);
        }
        _1f1.push(_1ef);
        _1ec = false;
        _1ed = key;
    }
    return _1eb;
}, arrayLikeIter: function(_1f2) {
    var i = 0;
    return { repr: function() {
        return "arrayLikeIter(...)";
    }, toString: MochiKit.Base.forwardCall("repr"), next: function() {
        if (i >= _1f2.length) {
            throw MochiKit.Iter.StopIteration;
        }
        return _1f2[i++];
    } 
    };
}, hasIterateNext: function(_1f4) {
    return (_1f4 && typeof (_1f4.iterateNext) == "function");
}, iterateNextIter: function(_1f5) {
    return { repr: function() {
        return "iterateNextIter(...)";
    }, toString: MochiKit.Base.forwardCall("repr"), next: function() {
        var rval = _1f5.iterateNext();
        if (rval === null || rval === undefined) {
            throw MochiKit.Iter.StopIteration;
        }
        return rval;
    } 
    };
} 
});
MochiKit.Iter.EXPORT_OK = ["iteratorRegistry", "arrayLikeIter", "hasIterateNext", "iterateNextIter"];
MochiKit.Iter.EXPORT = ["StopIteration", "registerIteratorFactory", "iter", "count", "cycle", "repeat", "next", "izip", "ifilter", "ifilterfalse", "islice", "imap", "applymap", "chain", "takewhile", "dropwhile", "tee", "list", "reduce", "range", "sum", "exhaust", "forEach", "every", "sorted", "reversed", "some", "iextend", "groupby", "groupby_as_array"];
MochiKit.Iter.__new__ = function() {
    var m = MochiKit.Base;
    if (typeof (StopIteration) != "undefined") {
        this.StopIteration = StopIteration;
    } else {
        this.StopIteration = new m.NamedError("StopIteration");
    }
    this.iteratorRegistry = new m.AdapterRegistry();
    this.registerIteratorFactory("arrayLike", m.isArrayLike, this.arrayLikeIter);
    this.registerIteratorFactory("iterateNext", this.hasIterateNext, this.iterateNextIter);
    this.EXPORT_TAGS = { ":common": this.EXPORT, ":all": m.concat(this.EXPORT, this.EXPORT_OK) };
    m.nameFunctions(this);
};
MochiKit.Iter.__new__();
if (MochiKit.__export__) {
    reduce = MochiKit.Iter.reduce;
}
MochiKit.Base._exportSymbols(this, MochiKit.Iter);
MochiKit.Base._deps("Logging", ["Base"]);
MochiKit.Logging.NAME = "MochiKit.Logging";
MochiKit.Logging.VERSION = "1.4.2";
MochiKit.Logging.__repr__ = function() {
    return "[" + this.NAME + " " + this.VERSION + "]";
};
MochiKit.Logging.toString = function() {
    return this.__repr__();
};
MochiKit.Logging.EXPORT = ["LogLevel", "LogMessage", "Logger", "alertListener", "logger", "log", "logError", "logDebug", "logFatal", "logWarning"];
MochiKit.Logging.EXPORT_OK = ["logLevelAtLeast", "isLogMessage", "compareLogMessage"];
MochiKit.Logging.LogMessage = function(num, _1f9, info) {
    this.num = num;
    this.level = _1f9;
    this.info = info;
    this.timestamp = new Date();
};
MochiKit.Logging.LogMessage.prototype = { repr: function() {
    var m = MochiKit.Base;
    return "LogMessage(" + m.map(m.repr, [this.num, this.level, this.info]).join(", ") + ")";
}, toString: MochiKit.Base.forwardCall("repr")
};
MochiKit.Base.update(MochiKit.Logging, { logLevelAtLeast: function(_1fc) {
    var self = MochiKit.Logging;
    if (typeof (_1fc) == "string") {
        _1fc = self.LogLevel[_1fc];
    }
    return function(msg) {
        var _1ff = msg.level;
        if (typeof (_1ff) == "string") {
            _1ff = self.LogLevel[_1ff];
        }
        return _1ff >= _1fc;
    };
}, isLogMessage: function() {
    var _200 = MochiKit.Logging.LogMessage;
    for (var i = 0; i < arguments.length; i++) {
        if (!(arguments[i] instanceof _200)) {
            return false;
        }
    }
    return true;
}, compareLogMessage: function(a, b) {
    return MochiKit.Base.compare([a.level, a.info], [b.level, b.info]);
}, alertListener: function(msg) {
    alert("num: " + msg.num + "\nlevel: " + msg.level + "\ninfo: " + msg.info.join(" "));
} 
});
MochiKit.Logging.Logger = function(_205) {
    this.counter = 0;
    if (typeof (_205) == "undefined" || _205 === null) {
        _205 = -1;
    }
    this.maxSize = _205;
    this._messages = [];
    this.listeners = {};
    this.useNativeConsole = false;
};
MochiKit.Logging.Logger.prototype = { clear: function() {
    this._messages.splice(0, this._messages.length);
}, logToConsole: function(msg) {
    if (typeof (window) != "undefined" && window.console && window.console.log) {
        window.console.log(msg.replace(/%/g, "\uff05"));
    } else {
        if (typeof (opera) != "undefined" && opera.postError) {
            opera.postError(msg);
        } else {
            if (typeof (printfire) == "function") {
                printfire(msg);
            } else {
                if (typeof (Debug) != "undefined" && Debug.writeln) {
                    Debug.writeln(msg);
                } else {
                    if (typeof (debug) != "undefined" && debug.trace) {
                        debug.trace(msg);
                    }
                }
            }
        }
    }
}, dispatchListeners: function(msg) {
    for (var k in this.listeners) {
        var pair = this.listeners[k];
        if (pair.ident != k || (pair[0] && !pair[0](msg))) {
            continue;
        }
        pair[1](msg);
    }
}, addListener: function(_20a, _20b, _20c) {
    if (typeof (_20b) == "string") {
        _20b = MochiKit.Logging.logLevelAtLeast(_20b);
    }
    var _20d = [_20b, _20c];
    _20d.ident = _20a;
    this.listeners[_20a] = _20d;
}, removeListener: function(_20e) {
    delete this.listeners[_20e];
}, baseLog: function(_20f, _210) {
    if (typeof (_20f) == "number") {
        if (_20f >= MochiKit.Logging.LogLevel.FATAL) {
            _20f = "FATAL";
        } else {
            if (_20f >= MochiKit.Logging.LogLevel.ERROR) {
                _20f = "ERROR";
            } else {
                if (_20f >= MochiKit.Logging.LogLevel.WARNING) {
                    _20f = "WARNING";
                } else {
                    if (_20f >= MochiKit.Logging.LogLevel.INFO) {
                        _20f = "INFO";
                    } else {
                        _20f = "DEBUG";
                    }
                }
            }
        }
    }
    var msg = new MochiKit.Logging.LogMessage(this.counter, _20f, MochiKit.Base.extend(null, arguments, 1));
    this._messages.push(msg);
    this.dispatchListeners(msg);
    if (this.useNativeConsole) {
        this.logToConsole(msg.level + ": " + msg.info.join(" "));
    }
    this.counter += 1;
    while (this.maxSize >= 0 && this._messages.length > this.maxSize) {
        this._messages.shift();
    }
}, getMessages: function(_212) {
    var _213 = 0;
    if (!(typeof (_212) == "undefined" || _212 === null)) {
        _213 = Math.max(0, this._messages.length - _212);
    }
    return this._messages.slice(_213);
}, getMessageText: function(_214) {
    if (typeof (_214) == "undefined" || _214 === null) {
        _214 = 30;
    }
    var _215 = this.getMessages(_214);
    if (_215.length) {
        var lst = map(function(m) {
            return "\n  [" + m.num + "] " + m.level + ": " + m.info.join(" ");
        }, _215);
        lst.unshift("LAST " + _215.length + " MESSAGES:");
        return lst.join("");
    }
    return "";
}, debuggingBookmarklet: function(_218) {
    if (typeof (MochiKit.LoggingPane) == "undefined") {
        alert(this.getMessageText());
    } else {
        MochiKit.LoggingPane.createLoggingPane(_218 || false);
    }
} 
};
MochiKit.Logging.__new__ = function() {
    this.LogLevel = { ERROR: 40, FATAL: 50, WARNING: 30, INFO: 20, DEBUG: 10 };
    var m = MochiKit.Base;
    m.registerComparator("LogMessage", this.isLogMessage, this.compareLogMessage);
    var _21a = m.partial;
    var _21b = this.Logger;
    var _21c = _21b.prototype.baseLog;
    m.update(this.Logger.prototype, { debug: _21a(_21c, "DEBUG"), log: _21a(_21c, "INFO"), error: _21a(_21c, "ERROR"), fatal: _21a(_21c, "FATAL"), warning: _21a(_21c, "WARNING") });
    var self = this;
    var _21e = function(name) {
        return function() {
            self.logger[name].apply(self.logger, arguments);
        };
    };
    this.log = _21e("log");
    this.logError = _21e("error");
    this.logDebug = _21e("debug");
    this.logFatal = _21e("fatal");
    this.logWarning = _21e("warning");
    this.logger = new _21b();
    this.logger.useNativeConsole = true;
    this.EXPORT_TAGS = { ":common": this.EXPORT, ":all": m.concat(this.EXPORT, this.EXPORT_OK) };
    m.nameFunctions(this);
};
if (typeof (printfire) == "undefined" && typeof (document) != "undefined" && document.createEvent && typeof (dispatchEvent) != "undefined") {
    printfire = function() {
        printfire.args = arguments;
        var ev = document.createEvent("Events");
        ev.initEvent("printfire", false, true);
        dispatchEvent(ev);
    };
}
MochiKit.Logging.__new__();
MochiKit.Base._exportSymbols(this, MochiKit.Logging);
MochiKit.Base._deps("DateTime", ["Base"]);
MochiKit.DateTime.NAME = "MochiKit.DateTime";
MochiKit.DateTime.VERSION = "1.4.2";
MochiKit.DateTime.__repr__ = function() {
    return "[" + this.NAME + " " + this.VERSION + "]";
};
MochiKit.DateTime.toString = function() {
    return this.__repr__();
};
MochiKit.DateTime.isoDate = function(str) {
    str = str + "";
    if (typeof (str) != "string" || str.length === 0) {
        return null;
    }
    var iso = str.split("-");
    if (iso.length === 0) {
        return null;
    }
    var date = new Date(iso[0], iso[1] - 1, iso[2]);
    date.setFullYear(iso[0]);
    date.setMonth(iso[1] - 1);
    date.setDate(iso[2]);
    return date;
};
MochiKit.DateTime._isoRegexp = /(\d{4,})(?:-(\d{1,2})(?:-(\d{1,2})(?:[T ](\d{1,2}):(\d{1,2})(?::(\d{1,2})(?:\.(\d+))?)?(?:(Z)|([+-])(\d{1,2})(?::(\d{1,2}))?)?)?)?)?/;
MochiKit.DateTime.isoTimestamp = function(str) {
    str = str + "";
    if (typeof (str) != "string" || str.length === 0) {
        return null;
    }
    var res = str.match(MochiKit.DateTime._isoRegexp);
    if (typeof (res) == "undefined" || res === null) {
        return null;
    }
    var year, _227, day, hour, min, sec, msec;
    year = parseInt(res[1], 10);
    if (typeof (res[2]) == "undefined" || res[2] === "") {
        return new Date(year);
    }
    _227 = parseInt(res[2], 10) - 1;
    day = parseInt(res[3], 10);
    if (typeof (res[4]) == "undefined" || res[4] === "") {
        return new Date(year, _227, day);
    }
    hour = parseInt(res[4], 10);
    min = parseInt(res[5], 10);
    sec = (typeof (res[6]) != "undefined" && res[6] !== "") ? parseInt(res[6], 10) : 0;
    if (typeof (res[7]) != "undefined" && res[7] !== "") {
        msec = Math.round(1000 * parseFloat("0." + res[7]));
    } else {
        msec = 0;
    }
    if ((typeof (res[8]) == "undefined" || res[8] === "") && (typeof (res[9]) == "undefined" || res[9] === "")) {
        return new Date(year, _227, day, hour, min, sec, msec);
    }
    var ofs;
    if (typeof (res[9]) != "undefined" && res[9] !== "") {
        ofs = parseInt(res[10], 10) * 3600000;
        if (typeof (res[11]) != "undefined" && res[11] !== "") {
            ofs += parseInt(res[11], 10) * 60000;
        }
        if (res[9] == "-") {
            ofs = -ofs;
        }
    } else {
        ofs = 0;
    }
    return new Date(Date.UTC(year, _227, day, hour, min, sec, msec) - ofs);
};
MochiKit.DateTime.toISOTime = function(date, _22f) {
    if (typeof (date) == "undefined" || date === null) {
        return null;
    }
    var hh = date.getHours();
    var mm = date.getMinutes();
    var ss = date.getSeconds();
    var lst = [((_22f && (hh < 10)) ? "0" + hh : hh), ((mm < 10) ? "0" + mm : mm), ((ss < 10) ? "0" + ss : ss)];
    return lst.join(":");
};
MochiKit.DateTime.toISOTimestamp = function(date, _235) {
    if (typeof (date) == "undefined" || date === null) {
        return null;
    }
    var sep = _235 ? "T" : " ";
    var foot = _235 ? "Z" : "";
    if (_235) {
        date = new Date(date.getTime() + (date.getTimezoneOffset() * 60000));
    }
    return MochiKit.DateTime.toISODate(date) + sep + MochiKit.DateTime.toISOTime(date, _235) + foot;
};
MochiKit.DateTime.toISODate = function(date) {
    if (typeof (date) == "undefined" || date === null) {
        return null;
    }
    var _239 = MochiKit.DateTime._padTwo;
    var _23a = MochiKit.DateTime._padFour;
    return [_23a(date.getFullYear()), _239(date.getMonth() + 1), _239(date.getDate())].join("-");
};
MochiKit.DateTime.americanDate = function(d) {
    d = d + "";
    if (typeof (d) != "string" || d.length === 0) {
        return null;
    }
    var a = d.split("/");
    return new Date(a[2], a[0] - 1, a[1]);
};
MochiKit.DateTime._padTwo = function(n) {
    return (n > 9) ? n : "0" + n;
};
MochiKit.DateTime._padFour = function(n) {
    switch (n.toString().length) {
        case 1:
            return "000" + n;
            break;
        case 2:
            return "00" + n;
            break;
        case 3:
            return "0" + n;
            break;
        case 4:
        default:
            return n;
    }
};
MochiKit.DateTime.toPaddedAmericanDate = function(d) {
    if (typeof (d) == "undefined" || d === null) {
        return null;
    }
    var _240 = MochiKit.DateTime._padTwo;
    return [_240(d.getMonth() + 1), _240(d.getDate()), d.getFullYear()].join("/");
};
MochiKit.DateTime.toAmericanDate = function(d) {
    if (typeof (d) == "undefined" || d === null) {
        return null;
    }
    return [d.getMonth() + 1, d.getDate(), d.getFullYear()].join("/");
};
MochiKit.DateTime.EXPORT = ["isoDate", "isoTimestamp", "toISOTime", "toISOTimestamp", "toISODate", "americanDate", "toPaddedAmericanDate", "toAmericanDate"];
MochiKit.DateTime.EXPORT_OK = [];
MochiKit.DateTime.EXPORT_TAGS = { ":common": MochiKit.DateTime.EXPORT, ":all": MochiKit.DateTime.EXPORT };
MochiKit.DateTime.__new__ = function() {
    var base = this.NAME + ".";
    for (var k in this) {
        var o = this[k];
        if (typeof (o) == "function" && typeof (o.NAME) == "undefined") {
            try {
                o.NAME = base + k;
            }
            catch (e) {
            }
        }
    }
};
MochiKit.DateTime.__new__();
if (typeof (MochiKit.Base) != "undefined") {
    MochiKit.Base._exportSymbols(this, MochiKit.DateTime);
} else {
    (function(_245, _246) {
        if ((typeof (JSAN) == "undefined" && typeof (dojo) == "undefined") || (MochiKit.__export__ === false)) {
            var all = _246.EXPORT_TAGS[":all"];
            for (var i = 0; i < all.length; i++) {
                _245[all[i]] = _246[all[i]];
            }
        }
    })(this, MochiKit.DateTime);
}
MochiKit.Base._deps("Format", ["Base"]);
MochiKit.Format.NAME = "MochiKit.Format";
MochiKit.Format.VERSION = "1.4.2";
MochiKit.Format.__repr__ = function() {
    return "[" + this.NAME + " " + this.VERSION + "]";
};
MochiKit.Format.toString = function() {
    return this.__repr__();
};
MochiKit.Format._numberFormatter = function(_249, _24a, _24b, _24c, _24d, _24e, _24f, _250, _251) {
    return function(num) {
        num = parseFloat(num);
        if (typeof (num) == "undefined" || num === null || isNaN(num)) {
            return _249;
        }
        var _253 = _24a;
        var _254 = _24b;
        if (num < 0) {
            num = -num;
        } else {
            _253 = _253.replace(/-/, "");
        }
        var me = arguments.callee;
        var fmt = MochiKit.Format.formatLocale(_24c);
        if (_24d) {
            num = num * 100;
            _254 = fmt.percent + _254;
        }
        num = MochiKit.Format.roundToFixed(num, _24e);
        var _257 = num.split(/\./);
        var _258 = _257[0];
        var frac = (_257.length == 1) ? "" : _257[1];
        var res = "";
        while (_258.length < _24f) {
            _258 = "0" + _258;
        }
        if (_250) {
            while (_258.length > _250) {
                var i = _258.length - _250;
                res = fmt.separator + _258.substring(i, _258.length) + res;
                _258 = _258.substring(0, i);
            }
        }
        res = _258 + res;
        if (_24e > 0) {
            while (frac.length < _251) {
                frac = frac + "0";
            }
            res = res + fmt.decimal + frac;
        }
        return _253 + res + _254;
    };
};
MochiKit.Format.numberFormatter = function(_25c, _25d, _25e) {
    if (typeof (_25d) == "undefined") {
        _25d = "";
    }
    var _25f = _25c.match(/((?:[0#]+,)?[0#]+)(?:\.([0#]+))?(%)?/);
    if (!_25f) {
        throw TypeError("Invalid pattern");
    }
    var _260 = _25c.substr(0, _25f.index);
    var _261 = _25c.substr(_25f.index + _25f[0].length);
    if (_260.search(/-/) == -1) {
        _260 = _260 + "-";
    }
    var _262 = _25f[1];
    var frac = (typeof (_25f[2]) == "string" && _25f[2] != "") ? _25f[2] : "";
    var _264 = (typeof (_25f[3]) == "string" && _25f[3] != "");
    var tmp = _262.split(/,/);
    var _266;
    if (typeof (_25e) == "undefined") {
        _25e = "default";
    }
    if (tmp.length == 1) {
        _266 = null;
    } else {
        _266 = tmp[1].length;
    }
    var _267 = _262.length - _262.replace(/0/g, "").length;
    var _268 = frac.length - frac.replace(/0/g, "").length;
    var _269 = frac.length;
    var rval = MochiKit.Format._numberFormatter(_25d, _260, _261, _25e, _264, _269, _267, _266, _268);
    var m = MochiKit.Base;
    if (m) {
        var fn = arguments.callee;
        var args = m.concat(arguments);
        rval.repr = function() {
            return [self.NAME, "(", map(m.repr, args).join(", "), ")"].join("");
        };
    }
    return rval;
};
MochiKit.Format.formatLocale = function(_26e) {
    if (typeof (_26e) == "undefined" || _26e === null) {
        _26e = "default";
    }
    if (typeof (_26e) == "string") {
        var rval = MochiKit.Format.LOCALE[_26e];
        if (typeof (rval) == "string") {
            rval = arguments.callee(rval);
            MochiKit.Format.LOCALE[_26e] = rval;
        }
        return rval;
    } else {
        return _26e;
    }
};
MochiKit.Format.twoDigitAverage = function(_270, _271) {
    if (_271) {
        var res = _270 / _271;
        if (!isNaN(res)) {
            return MochiKit.Format.twoDigitFloat(res);
        }
    }
    return "0";
};
MochiKit.Format.twoDigitFloat = function(_273) {
    var res = roundToFixed(_273, 2);
    if (res.indexOf(".00") > 0) {
        return res.substring(0, res.length - 3);
    } else {
        if (res.charAt(res.length - 1) == "0") {
            return res.substring(0, res.length - 1);
        } else {
            return res;
        }
    }
};
MochiKit.Format.lstrip = function(str, _276) {
    str = str + "";
    if (typeof (str) != "string") {
        return null;
    }
    if (!_276) {
        return str.replace(/^\s+/, "");
    } else {
        return str.replace(new RegExp("^[" + _276 + "]+"), "");
    }
};
MochiKit.Format.rstrip = function(str, _278) {
    str = str + "";
    if (typeof (str) != "string") {
        return null;
    }
    if (!_278) {
        return str.replace(/\s+$/, "");
    } else {
        return str.replace(new RegExp("[" + _278 + "]+$"), "");
    }
};
MochiKit.Format.strip = function(str, _27a) {
    var self = MochiKit.Format;
    return self.rstrip(self.lstrip(str, _27a), _27a);
};
MochiKit.Format.truncToFixed = function(_27c, _27d) {
    var res = Math.floor(_27c).toFixed(0);
    if (_27c < 0) {
        res = Math.ceil(_27c).toFixed(0);
        if (res.charAt(0) != "-" && _27d > 0) {
            res = "-" + res;
        }
    }
    if (res.indexOf("e") < 0 && _27d > 0) {
        var tail = _27c.toString();
        if (tail.indexOf("e") > 0) {
            tail = ".";
        } else {
            if (tail.indexOf(".") < 0) {
                tail = ".";
            } else {
                tail = tail.substring(tail.indexOf("."));
            }
        }
        if (tail.length - 1 > _27d) {
            tail = tail.substring(0, _27d + 1);
        }
        while (tail.length - 1 < _27d) {
            tail += "0";
        }
        res += tail;
    }
    return res;
};
MochiKit.Format.roundToFixed = function(_280, _281) {
    var _282 = Math.abs(_280) + 0.5 * Math.pow(10, -_281);
    var res = MochiKit.Format.truncToFixed(_282, _281);
    if (_280 < 0) {
        res = "-" + res;
    }
    return res;
};
MochiKit.Format.percentFormat = function(_284) {
    return MochiKit.Format.twoDigitFloat(100 * _284) + "%";
};
MochiKit.Format.EXPORT = ["truncToFixed", "roundToFixed", "numberFormatter", "formatLocale", "twoDigitAverage", "twoDigitFloat", "percentFormat", "lstrip", "rstrip", "strip"];
MochiKit.Format.LOCALE = { en_US: { separator: ",", decimal: ".", percent: "%" }, de_DE: { separator: ".", decimal: ",", percent: "%" }, pt_BR: { separator: ".", decimal: ",", percent: "%" }, fr_FR: { separator: " ", decimal: ",", percent: "%" }, "default": "en_US" };
MochiKit.Format.EXPORT_OK = [];
MochiKit.Format.EXPORT_TAGS = { ":all": MochiKit.Format.EXPORT, ":common": MochiKit.Format.EXPORT };
MochiKit.Format.__new__ = function() {
    var base = this.NAME + ".";
    var k, v, o;
    for (k in this.LOCALE) {
        o = this.LOCALE[k];
        if (typeof (o) == "object") {
            o.repr = function() {
                return this.NAME;
            };
            o.NAME = base + "LOCALE." + k;
        }
    }
    for (k in this) {
        o = this[k];
        if (typeof (o) == "function" && typeof (o.NAME) == "undefined") {
            try {
                o.NAME = base + k;
            }
            catch (e) {
            }
        }
    }
};
MochiKit.Format.__new__();
if (typeof (MochiKit.Base) != "undefined") {
    MochiKit.Base._exportSymbols(this, MochiKit.Format);
} else {
    (function(_289, _28a) {
        if ((typeof (JSAN) == "undefined" && typeof (dojo) == "undefined") || (MochiKit.__export__ === false)) {
            var all = _28a.EXPORT_TAGS[":all"];
            for (var i = 0; i < all.length; i++) {
                _289[all[i]] = _28a[all[i]];
            }
        }
    })(this, MochiKit.Format);
}
MochiKit.Base._deps("Async", ["Base"]);
MochiKit.Async.NAME = "MochiKit.Async";
MochiKit.Async.VERSION = "1.4.2";
MochiKit.Async.__repr__ = function() {
    return "[" + this.NAME + " " + this.VERSION + "]";
};
MochiKit.Async.toString = function() {
    return this.__repr__();
};
MochiKit.Async.Deferred = function(_28d) {
    this.chain = [];
    this.id = this._nextId();
    this.fired = -1;
    this.paused = 0;
    this.results = [null, null];
    this.canceller = _28d;
    this.silentlyCancelled = false;
    this.chained = false;
};
MochiKit.Async.Deferred.prototype = { repr: function() {
    var _28e;
    if (this.fired == -1) {
        _28e = "unfired";
    } else {
        if (this.fired === 0) {
            _28e = "success";
        } else {
            _28e = "error";
        }
    }
    return "Deferred(" + this.id + ", " + _28e + ")";
}, toString: MochiKit.Base.forwardCall("repr"), _nextId: MochiKit.Base.counter(), cancel: function() {
    var self = MochiKit.Async;
    if (this.fired == -1) {
        if (this.canceller) {
            this.canceller(this);
        } else {
            this.silentlyCancelled = true;
        }
        if (this.fired == -1) {
            this.errback(new self.CancelledError(this));
        }
    } else {
        if ((this.fired === 0) && (this.results[0] instanceof self.Deferred)) {
            this.results[0].cancel();
        }
    }
}, _resback: function(res) {
    this.fired = ((res instanceof Error) ? 1 : 0);
    this.results[this.fired] = res;
    this._fire();
}, _check: function() {
    if (this.fired != -1) {
        if (!this.silentlyCancelled) {
            throw new MochiKit.Async.AlreadyCalledError(this);
        }
        this.silentlyCancelled = false;
        return;
    }
}, callback: function(res) {
    this._check();
    if (res instanceof MochiKit.Async.Deferred) {
        throw new Error("Deferred instances can only be chained if they are the result of a callback");
    }
    this._resback(res);
}, errback: function(res) {
    this._check();
    var self = MochiKit.Async;
    if (res instanceof self.Deferred) {
        throw new Error("Deferred instances can only be chained if they are the result of a callback");
    }
    if (!(res instanceof Error)) {
        res = new self.GenericError(res);
    }
    this._resback(res);
}, addBoth: function(fn) {
    if (arguments.length > 1) {
        fn = MochiKit.Base.partial.apply(null, arguments);
    }
    return this.addCallbacks(fn, fn);
}, addCallback: function(fn) {
    if (arguments.length > 1) {
        fn = MochiKit.Base.partial.apply(null, arguments);
    }
    return this.addCallbacks(fn, null);
}, addErrback: function(fn) {
    if (arguments.length > 1) {
        fn = MochiKit.Base.partial.apply(null, arguments);
    }
    return this.addCallbacks(null, fn);
}, addCallbacks: function(cb, eb) {
    if (this.chained) {
        throw new Error("Chained Deferreds can not be re-used");
    }
    this.chain.push([cb, eb]);
    if (this.fired >= 0) {
        this._fire();
    }
    return this;
}, _fire: function() {
    var _299 = this.chain;
    var _29a = this.fired;
    var res = this.results[_29a];
    var self = this;
    var cb = null;
    while (_299.length > 0 && this.paused === 0) {
        var pair = _299.shift();
        var f = pair[_29a];
        if (f === null) {
            continue;
        }
        try {
            res = f(res);
            _29a = ((res instanceof Error) ? 1 : 0);
            if (res instanceof MochiKit.Async.Deferred) {
                cb = function(res) {
                    self._resback(res);
                    self.paused--;
                    if ((self.paused === 0) && (self.fired >= 0)) {
                        self._fire();
                    }
                };
                this.paused++;
            }
        }
        catch (err) {
            _29a = 1;
            if (!(err instanceof Error)) {
                err = new MochiKit.Async.GenericError(err);
            }
            res = err;
        }
    }
    this.fired = _29a;
    this.results[_29a] = res;
    if (cb && this.paused) {
        res.addBoth(cb);
        res.chained = true;
    }
} 
};
MochiKit.Base.update(MochiKit.Async, { evalJSONRequest: function(req) {
    return MochiKit.Base.evalJSON(req.responseText);
}, succeed: function(_2a2) {
    var d = new MochiKit.Async.Deferred();
    d.callback.apply(d, arguments);
    return d;
}, fail: function(_2a4) {
    var d = new MochiKit.Async.Deferred();
    d.errback.apply(d, arguments);
    return d;
}, getXMLHttpRequest: function() {
    var self = arguments.callee;
    if (!self.XMLHttpRequest) {
        var _2a7 = [function() {
            return new XMLHttpRequest();
        }, function() {
            return new ActiveXObject("Msxml2.XMLHTTP");
        }, function() {
            return new ActiveXObject("Microsoft.XMLHTTP");
        }, function() {
            return new ActiveXObject("Msxml2.XMLHTTP.4.0");
        }, function() {
            throw new MochiKit.Async.BrowserComplianceError("Browser does not support XMLHttpRequest");
        } ];
        for (var i = 0; i < _2a7.length; i++) {
            var func = _2a7[i];
            try {
                self.XMLHttpRequest = func;
                return func();
            }
            catch (e) {
            }
        }
    }
    return self.XMLHttpRequest();
}, _xhr_onreadystatechange: function(d) {
    var m = MochiKit.Base;
    if (this.readyState == 4) {
        try {
            this.onreadystatechange = null;
        }
        catch (e) {
            try {
                this.onreadystatechange = m.noop;
            }
            catch (e) {
            }
        }
        var _2ac = null;
        try {
            _2ac = this.status;
            if (!_2ac && m.isNotEmpty(this.responseText)) {
                _2ac = 304;
            }
        }
        catch (e) {
        }
        if (_2ac == 200 || _2ac == 201 || _2ac == 204 || _2ac == 304 || _2ac == 1223) {
            d.callback(this);
        } else {
            var err = new MochiKit.Async.XMLHttpRequestError(this, "Request failed");
            if (err.number) {
                d.errback(err);
            } else {
                d.errback(err);
            }
        }
    }
}, _xhr_canceller: function(req) {
    try {
        req.onreadystatechange = null;
    }
    catch (e) {
        try {
            req.onreadystatechange = MochiKit.Base.noop;
        }
        catch (e) {
        }
    }
    req.abort();
}, sendXMLHttpRequest: function(req, _2b0) {
    if (typeof (_2b0) == "undefined" || _2b0 === null) {
        _2b0 = "";
    }
    var m = MochiKit.Base;
    var self = MochiKit.Async;
    var d = new self.Deferred(m.partial(self._xhr_canceller, req));
    try {
        req.onreadystatechange = m.bind(self._xhr_onreadystatechange, req, d);
        req.send(_2b0);
    }
    catch (e) {
        try {
            req.onreadystatechange = null;
        }
        catch (ignore) {
        }
        d.errback(e);
    }
    return d;
}, doXHR: function(url, opts) {
    var self = MochiKit.Async;
    return self.callLater(0, self._doXHR, url, opts);
}, _doXHR: function(url, opts) {
    var m = MochiKit.Base;
    opts = m.update({ method: "GET", sendContent: "" }, opts);
    var self = MochiKit.Async;
    var req = self.getXMLHttpRequest();
    if (opts.queryString) {
        var qs = m.queryString(opts.queryString);
        if (qs) {
            url += "?" + qs;
        }
    }
    if ("username" in opts) {
        req.open(opts.method, url, true, opts.username, opts.password);
    } else {
        req.open(opts.method, url, true);
    }
    if (req.overrideMimeType && opts.mimeType) {
        req.overrideMimeType(opts.mimeType);
    }
    req.setRequestHeader("X-Requested-With", "XMLHttpRequest");
    if (opts.headers) {
        var _2bd = opts.headers;
        if (!m.isArrayLike(_2bd)) {
            _2bd = m.items(_2bd);
        }
        for (var i = 0; i < _2bd.length; i++) {
            var _2bf = _2bd[i];
            var name = _2bf[0];
            var _2c1 = _2bf[1];
            req.setRequestHeader(name, _2c1);
        }
    }
    return self.sendXMLHttpRequest(req, opts.sendContent);
}, _buildURL: function(url) {
    if (arguments.length > 1) {
        var m = MochiKit.Base;
        var qs = m.queryString.apply(null, m.extend(null, arguments, 1));
        if (qs) {
            return url + "?" + qs;
        }
    }
    return url;
}, doSimpleXMLHttpRequest: function(url) {
    var self = MochiKit.Async;
    url = self._buildURL.apply(self, arguments);
    return self.doXHR(url);
}, loadJSONDoc: function(url) {
    var self = MochiKit.Async;
    url = self._buildURL.apply(self, arguments);
    var d = self.doXHR(url, { "mimeType": "text/plain", "headers": [["Accept", "application/json"]] });
    d = d.addCallback(self.evalJSONRequest);
    return d;
}, wait: function(_2ca, _2cb) {
    var d = new MochiKit.Async.Deferred();
    var m = MochiKit.Base;
    if (typeof (_2cb) != "undefined") {
        d.addCallback(function() {
            return _2cb;
        });
    }
    var _2ce = setTimeout(m.bind("callback", d), Math.floor(_2ca * 1000));
    d.canceller = function() {
        try {
            clearTimeout(_2ce);
        }
        catch (e) {
        }
    };
    return d;
}, callLater: function(_2cf, func) {
    var m = MochiKit.Base;
    var _2d2 = m.partial.apply(m, m.extend(null, arguments, 1));
    return MochiKit.Async.wait(_2cf).addCallback(function(res) {
        return _2d2();
    });
} 
});
MochiKit.Async.DeferredLock = function() {
    this.waiting = [];
    this.locked = false;
    this.id = this._nextId();
};
MochiKit.Async.DeferredLock.prototype = { __class__: MochiKit.Async.DeferredLock, acquire: function() {
    var d = new MochiKit.Async.Deferred();
    if (this.locked) {
        this.waiting.push(d);
    } else {
        this.locked = true;
        d.callback(this);
    }
    return d;
}, release: function() {
    if (!this.locked) {
        throw TypeError("Tried to release an unlocked DeferredLock");
    }
    this.locked = false;
    if (this.waiting.length > 0) {
        this.locked = true;
        this.waiting.shift().callback(this);
    }
}, _nextId: MochiKit.Base.counter(), repr: function() {
    var _2d5;
    if (this.locked) {
        _2d5 = "locked, " + this.waiting.length + " waiting";
    } else {
        _2d5 = "unlocked";
    }
    return "DeferredLock(" + this.id + ", " + _2d5 + ")";
}, toString: MochiKit.Base.forwardCall("repr")
};
MochiKit.Async.DeferredList = function(list, _2d7, _2d8, _2d9, _2da) {
    MochiKit.Async.Deferred.apply(this, [_2da]);
    this.list = list;
    var _2db = [];
    this.resultList = _2db;
    this.finishedCount = 0;
    this.fireOnOneCallback = _2d7;
    this.fireOnOneErrback = _2d8;
    this.consumeErrors = _2d9;
    var cb = MochiKit.Base.bind(this._cbDeferred, this);
    for (var i = 0; i < list.length; i++) {
        var d = list[i];
        _2db.push(undefined);
        d.addCallback(cb, i, true);
        d.addErrback(cb, i, false);
    }
    if (list.length === 0 && !_2d7) {
        this.callback(this.resultList);
    }
};
MochiKit.Async.DeferredList.prototype = new MochiKit.Async.Deferred();
MochiKit.Async.DeferredList.prototype._cbDeferred = function(_2df, _2e0, _2e1) {
    this.resultList[_2df] = [_2e0, _2e1];
    this.finishedCount += 1;
    if (this.fired == -1) {
        if (_2e0 && this.fireOnOneCallback) {
            this.callback([_2df, _2e1]);
        } else {
            if (!_2e0 && this.fireOnOneErrback) {
                this.errback(_2e1);
            } else {
                if (this.finishedCount == this.list.length) {
                    this.callback(this.resultList);
                }
            }
        }
    }
    if (!_2e0 && this.consumeErrors) {
        _2e1 = null;
    }
    return _2e1;
};
MochiKit.Async.gatherResults = function(_2e2) {
    var d = new MochiKit.Async.DeferredList(_2e2, false, true, false);
    d.addCallback(function(_2e4) {
        var ret = [];
        for (var i = 0; i < _2e4.length; i++) {
            ret.push(_2e4[i][1]);
        }
        return ret;
    });
    return d;
};
MochiKit.Async.maybeDeferred = function(func) {
    var self = MochiKit.Async;
    var _2e9;
    try {
        var r = func.apply(null, MochiKit.Base.extend([], arguments, 1));
        if (r instanceof self.Deferred) {
            _2e9 = r;
        } else {
            if (r instanceof Error) {
                _2e9 = self.fail(r);
            } else {
                _2e9 = self.succeed(r);
            }
        }
    }
    catch (e) {
        _2e9 = self.fail(e);
    }
    return _2e9;
};
MochiKit.Async.EXPORT = ["AlreadyCalledError", "CancelledError", "BrowserComplianceError", "GenericError", "XMLHttpRequestError", "Deferred", "succeed", "fail", "getXMLHttpRequest", "doSimpleXMLHttpRequest", "loadJSONDoc", "wait", "callLater", "sendXMLHttpRequest", "DeferredLock", "DeferredList", "gatherResults", "maybeDeferred", "doXHR"];
MochiKit.Async.EXPORT_OK = ["evalJSONRequest"];
MochiKit.Async.__new__ = function() {
    var m = MochiKit.Base;
    var ne = m.partial(m._newNamedError, this);
    ne("AlreadyCalledError", function(_2ed) {
        this.deferred = _2ed;
    });
    ne("CancelledError", function(_2ee) {
        this.deferred = _2ee;
    });
    ne("BrowserComplianceError", function(msg) {
        this.message = msg;
    });
    ne("GenericError", function(msg) {
        this.message = msg;
    });
    ne("XMLHttpRequestError", function(req, msg) {
        this.req = req;
        this.message = msg;
        try {
            this.number = req.status;
        }
        catch (e) {
        }
    });
    this.EXPORT_TAGS = { ":common": this.EXPORT, ":all": m.concat(this.EXPORT, this.EXPORT_OK) };
    m.nameFunctions(this);
};
MochiKit.Async.__new__();
MochiKit.Base._exportSymbols(this, MochiKit.Async);
MochiKit.Base._deps("DOM", ["Base"]);
MochiKit.DOM.NAME = "MochiKit.DOM";
MochiKit.DOM.VERSION = "1.4.2";
MochiKit.DOM.__repr__ = function() {
    return "[" + this.NAME + " " + this.VERSION + "]";
};
MochiKit.DOM.toString = function() {
    return this.__repr__();
};
MochiKit.DOM.EXPORT = ["removeEmptyTextNodes", "formContents", "currentWindow", "currentDocument", "withWindow", "withDocument", "registerDOMConverter", "coerceToDOM", "createDOM", "createDOMFunc", "isChildNode", "getNodeAttribute", "removeNodeAttribute", "setNodeAttribute", "updateNodeAttributes", "appendChildNodes", "insertSiblingNodesAfter", "insertSiblingNodesBefore", "replaceChildNodes", "removeElement", "swapDOM", "BUTTON", "TT", "PRE", "H1", "H2", "H3", "H4", "H5", "H6", "BR", "CANVAS", "HR", "LABEL", "TEXTAREA", "FORM", "STRONG", "SELECT", "OPTION", "OPTGROUP", "LEGEND", "FIELDSET", "P", "UL", "OL", "LI", "DL", "DT", "DD", "TD", "TR", "THEAD", "TBODY", "TFOOT", "TABLE", "TH", "INPUT", "SPAN", "A", "DIV", "IMG", "getElement", "$", "getElementsByTagAndClassName", "addToCallStack", "addLoadEvent", "focusOnLoad", "setElementClass", "toggleElementClass", "addElementClass", "removeElementClass", "swapElementClass", "hasElementClass", "computedStyle", "escapeHTML", "toHTML", "emitHTML", "scrapeText", "getFirstParentByTagAndClassName", "getFirstElementByTagAndClassName"];
MochiKit.DOM.EXPORT_OK = ["domConverters"];
MochiKit.DOM.DEPRECATED = [["computedStyle", "MochiKit.Style.getStyle", "1.4"], ["elementDimensions", "MochiKit.Style.getElementDimensions", "1.4"], ["elementPosition", "MochiKit.Style.getElementPosition", "1.4"], ["getViewportDimensions", "MochiKit.Style.getViewportDimensions", "1.4"], ["hideElement", "MochiKit.Style.hideElement", "1.4"], ["makeClipping", "MochiKit.Style.makeClipping", "1.4.1"], ["makePositioned", "MochiKit.Style.makePositioned", "1.4.1"], ["setElementDimensions", "MochiKit.Style.setElementDimensions", "1.4"], ["setElementPosition", "MochiKit.Style.setElementPosition", "1.4"], ["setDisplayForElement", "MochiKit.Style.setDisplayForElement", "1.4"], ["setOpacity", "MochiKit.Style.setOpacity", "1.4"], ["showElement", "MochiKit.Style.showElement", "1.4"], ["undoClipping", "MochiKit.Style.undoClipping", "1.4.1"], ["undoPositioned", "MochiKit.Style.undoPositioned", "1.4.1"], ["Coordinates", "MochiKit.Style.Coordinates", "1.4"], ["Dimensions", "MochiKit.Style.Dimensions", "1.4"]];
MochiKit.Base.update(MochiKit.DOM, { currentWindow: function() {
    return MochiKit.DOM._window;
}, currentDocument: function() {
    return MochiKit.DOM._document;
}, withWindow: function(win, func) {
    var self = MochiKit.DOM;
    var _2f6 = self._document;
    var _2f7 = self._window;
    var rval;
    try {
        self._window = win;
        self._document = win.document;
        rval = func();
    }
    catch (e) {
        self._window = _2f7;
        self._document = _2f6;
        throw e;
    }
    self._window = _2f7;
    self._document = _2f6;
    return rval;
}, formContents: function(elem) {
    var _2fa = [];
    var _2fb = [];
    var m = MochiKit.Base;
    var self = MochiKit.DOM;
    if (typeof (elem) == "undefined" || elem === null) {
        elem = self._document.body;
    } else {
        elem = self.getElement(elem);
    }
    m.nodeWalk(elem, function(elem) {
        var name = elem.name;
        if (m.isNotEmpty(name)) {
            var _300 = elem.tagName.toUpperCase();
            if (_300 === "INPUT" && (elem.type == "radio" || elem.type == "checkbox") && !elem.checked) {
                return null;
            }
            if (_300 === "SELECT") {
                if (elem.type == "select-one") {
                    if (elem.selectedIndex >= 0) {
                        var opt = elem.options[elem.selectedIndex];
                        var v = opt.value;
                        if (!v) {
                            var h = opt.outerHTML;
                            if (h && !h.match(/^[^>]+\svalue\s*=/i)) {
                                v = opt.text;
                            }
                        }
                        _2fa.push(name);
                        _2fb.push(v);
                        return null;
                    }
                    _2fa.push(name);
                    _2fb.push("");
                    return null;
                } else {
                    var opts = elem.options;
                    if (!opts.length) {
                        _2fa.push(name);
                        _2fb.push("");
                        return null;
                    }
                    for (var i = 0; i < opts.length; i++) {
                        var opt = opts[i];
                        if (!opt.selected) {
                            continue;
                        }
                        var v = opt.value;
                        if (!v) {
                            var h = opt.outerHTML;
                            if (h && !h.match(/^[^>]+\svalue\s*=/i)) {
                                v = opt.text;
                            }
                        }
                        _2fa.push(name);
                        _2fb.push(v);
                    }
                    return null;
                }
            }
            if (_300 === "FORM" || _300 === "P" || _300 === "SPAN" || _300 === "DIV") {
                return elem.childNodes;
            }
            _2fa.push(name);
            _2fb.push(elem.value || "");
            return null;
        }
        return elem.childNodes;
    });
    return [_2fa, _2fb];
}, withDocument: function(doc, func) {
    var self = MochiKit.DOM;
    var _309 = self._document;
    var rval;
    try {
        self._document = doc;
        rval = func();
    }
    catch (e) {
        self._document = _309;
        throw e;
    }
    self._document = _309;
    return rval;
}, registerDOMConverter: function(name, _30c, wrap, _30e) {
    MochiKit.DOM.domConverters.register(name, _30c, wrap, _30e);
}, coerceToDOM: function(node, ctx) {
    var m = MochiKit.Base;
    var im = MochiKit.Iter;
    var self = MochiKit.DOM;
    if (im) {
        var iter = im.iter;
        var _315 = im.repeat;
    }
    var map = m.map;
    var _317 = self.domConverters;
    var _318 = arguments.callee;
    var _319 = m.NotFound;
    while (true) {
        if (typeof (node) == "undefined" || node === null) {
            return null;
        }
        if (typeof (node) == "function" && typeof (node.length) == "number" && !(node instanceof Function)) {
            node = im ? im.list(node) : m.extend(null, node);
        }
        if (typeof (node.nodeType) != "undefined" && node.nodeType > 0) {
            return node;
        }
        if (typeof (node) == "number" || typeof (node) == "boolean") {
            node = node.toString();
        }
        if (typeof (node) == "string") {
            return self._document.createTextNode(node);
        }
        if (typeof (node.__dom__) == "function") {
            node = node.__dom__(ctx);
            continue;
        }
        if (typeof (node.dom) == "function") {
            node = node.dom(ctx);
            continue;
        }
        if (typeof (node) == "function") {
            node = node.apply(ctx, [ctx]);
            continue;
        }
        if (im) {
            var _31a = null;
            try {
                _31a = iter(node);
            }
            catch (e) {
            }
            if (_31a) {
                return map(_318, _31a, _315(ctx));
            }
        } else {
            if (m.isArrayLike(node)) {
                var func = function(n) {
                    return _318(n, ctx);
                };
                return map(func, node);
            }
        }
        try {
            node = _317.match(node, ctx);
            continue;
        }
        catch (e) {
            if (e != _319) {
                throw e;
            }
        }
        return self._document.createTextNode(node.toString());
    }
    return undefined;
}, isChildNode: function(node, _31e) {
    var self = MochiKit.DOM;
    if (typeof (node) == "string") {
        node = self.getElement(node);
    }
    if (typeof (_31e) == "string") {
        _31e = self.getElement(_31e);
    }
    if (typeof (node) == "undefined" || node === null) {
        return false;
    }
    while (node != null && node !== self._document) {
        if (node === _31e) {
            return true;
        }
        node = node.parentNode;
    }
    return false;
}, setNodeAttribute: function(node, attr, _322) {
    var o = {};
    o[attr] = _322;
    try {
        return MochiKit.DOM.updateNodeAttributes(node, o);
    }
    catch (e) {
    }
    return null;
}, getNodeAttribute: function(node, attr) {
    var self = MochiKit.DOM;
    var _327 = self.attributeArray.renames[attr];
    var _328 = self.attributeArray.ignoreAttr[attr];
    node = self.getElement(node);
    try {
        if (_327) {
            return node[_327];
        }
        var _329 = node.getAttribute(attr);
        if (_329 != _328) {
            return _329;
        }
    }
    catch (e) {
    }
    return null;
}, removeNodeAttribute: function(node, attr) {
    var self = MochiKit.DOM;
    var _32d = self.attributeArray.renames[attr];
    node = self.getElement(node);
    try {
        if (_32d) {
            return node[_32d];
        }
        return node.removeAttribute(attr);
    }
    catch (e) {
    }
    return null;
}, updateNodeAttributes: function(node, _32f) {
    var elem = node;
    var self = MochiKit.DOM;
    if (typeof (node) == "string") {
        elem = self.getElement(node);
    }
    if (_32f) {
        var _332 = MochiKit.Base.updatetree;
        if (self.attributeArray.compliant) {
            for (var k in _32f) {
                var v = _32f[k];
                if (typeof (v) == "object" && typeof (elem[k]) == "object") {
                    if (k == "style" && MochiKit.Style) {
                        MochiKit.Style.setStyle(elem, v);
                    } else {
                        _332(elem[k], v);
                    }
                } else {
                    if (k.substring(0, 2) == "on") {
                        if (typeof (v) == "string") {
                            v = new Function(v);
                        }
                        elem[k] = v;
                    } else {
                        elem.setAttribute(k, v);
                    }
                }
                if (typeof (elem[k]) == "string" && elem[k] != v) {
                    elem[k] = v;
                }
            }
        } else {
            var _335 = self.attributeArray.renames;
            for (var k in _32f) {
                v = _32f[k];
                var _336 = _335[k];
                if (k == "style" && typeof (v) == "string") {
                    elem.style.cssText = v;
                } else {
                    if (typeof (_336) == "string") {
                        elem[_336] = v;
                    } else {
                        if (typeof (elem[k]) == "object" && typeof (v) == "object") {
                            if (k == "style" && MochiKit.Style) {
                                MochiKit.Style.setStyle(elem, v);
                            } else {
                                _332(elem[k], v);
                            }
                        } else {
                            if (k.substring(0, 2) == "on") {
                                if (typeof (v) == "string") {
                                    v = new Function(v);
                                }
                                elem[k] = v;
                            } else {
                                elem.setAttribute(k, v);
                            }
                        }
                    }
                }
                if (typeof (elem[k]) == "string" && elem[k] != v) {
                    elem[k] = v;
                }
            }
        }
    }
    return elem;
}, appendChildNodes: function(node) {
    var elem = node;
    var self = MochiKit.DOM;
    if (typeof (node) == "string") {
        elem = self.getElement(node);
    }
    var _33a = [self.coerceToDOM(MochiKit.Base.extend(null, arguments, 1), elem)];
    var _33b = MochiKit.Base.concat;
    while (_33a.length) {
        var n = _33a.shift();
        if (typeof (n) == "undefined" || n === null) {
        } else {
            if (typeof (n.nodeType) == "number") {
                elem.appendChild(n);
            } else {
                _33a = _33b(n, _33a);
            }
        }
    }
    return elem;
}, insertSiblingNodesBefore: function(node) {
    var elem = node;
    var self = MochiKit.DOM;
    if (typeof (node) == "string") {
        elem = self.getElement(node);
    }
    var _340 = [self.coerceToDOM(MochiKit.Base.extend(null, arguments, 1), elem)];
    var _341 = elem.parentNode;
    var _342 = MochiKit.Base.concat;
    while (_340.length) {
        var n = _340.shift();
        if (typeof (n) == "undefined" || n === null) {
        } else {
            if (typeof (n.nodeType) == "number") {
                _341.insertBefore(n, elem);
            } else {
                _340 = _342(n, _340);
            }
        }
    }
    return _341;
}, insertSiblingNodesAfter: function(node) {
    var elem = node;
    var self = MochiKit.DOM;
    if (typeof (node) == "string") {
        elem = self.getElement(node);
    }
    var _347 = [self.coerceToDOM(MochiKit.Base.extend(null, arguments, 1), elem)];
    if (elem.nextSibling) {
        return self.insertSiblingNodesBefore(elem.nextSibling, _347);
    } else {
        return self.appendChildNodes(elem.parentNode, _347);
    }
}, replaceChildNodes: function(node) {
    var elem = node;
    var self = MochiKit.DOM;
    if (typeof (node) == "string") {
        elem = self.getElement(node);
        arguments[0] = elem;
    }
    var _34b;
    while ((_34b = elem.firstChild)) {
        elem.removeChild(_34b);
    }
    if (arguments.length < 2) {
        return elem;
    } else {
        return self.appendChildNodes.apply(this, arguments);
    }
}, createDOM: function(name, _34d) {
    var elem;
    var self = MochiKit.DOM;
    var m = MochiKit.Base;
    if (typeof (_34d) == "string" || typeof (_34d) == "number") {
        var args = m.extend([name, null], arguments, 1);
        return arguments.callee.apply(this, args);
    }
    if (typeof (name) == "string") {
        var _352 = self._xhtml;
        if (_34d && !self.attributeArray.compliant) {
            var _353 = "";
            if ("name" in _34d) {
                _353 += " name=\"" + self.escapeHTML(_34d.name) + "\"";
            }
            if (name == "input" && "type" in _34d) {
                _353 += " type=\"" + self.escapeHTML(_34d.type) + "\"";
            }
            if (_353) {
                name = "<" + name + _353 + ">";
                _352 = false;
            }
        }
        var d = self._document;
        if (_352 && d === document) {
            elem = d.createElementNS("http://www.w3.org/1999/xhtml", name);
        } else {
            elem = d.createElement(name);
        }
    } else {
        elem = name;
    }
    if (_34d) {
        self.updateNodeAttributes(elem, _34d);
    }
    if (arguments.length <= 2) {
        return elem;
    } else {
        var args = m.extend([elem], arguments, 2);
        return self.appendChildNodes.apply(this, args);
    }
}, createDOMFunc: function() {
    var m = MochiKit.Base;
    return m.partial.apply(this, m.extend([MochiKit.DOM.createDOM], arguments));
}, removeElement: function(elem) {
    var self = MochiKit.DOM;
    var e = self.coerceToDOM(self.getElement(elem));
    e.parentNode.removeChild(e);
    return e;
}, swapDOM: function(dest, src) {
    var self = MochiKit.DOM;
    dest = self.getElement(dest);
    var _35c = dest.parentNode;
    if (src) {
        src = self.coerceToDOM(self.getElement(src), _35c);
        _35c.replaceChild(src, dest);
    } else {
        _35c.removeChild(dest);
    }
    return src;
}, getElement: function(id) {
    var self = MochiKit.DOM;
    if (arguments.length == 1) {
        return ((typeof (id) == "string") ? self._document.getElementById(id) : id);
    } else {
        return MochiKit.Base.map(self.getElement, arguments);
    }
}, getElementsByTagAndClassName: function(_35f, _360, _361) {
    var self = MochiKit.DOM;
    if (typeof (_35f) == "undefined" || _35f === null) {
        _35f = "*";
    }
    if (typeof (_361) == "undefined" || _361 === null) {
        _361 = self._document;
    }
    _361 = self.getElement(_361);
    if (_361 == null) {
        return [];
    }
    var _363 = (_361.getElementsByTagName(_35f) || self._document.all);
    if (typeof (_360) == "undefined" || _360 === null) {
        return MochiKit.Base.extend(null, _363);
    }
    var _364 = [];
    for (var i = 0; i < _363.length; i++) {
        var _366 = _363[i];
        var cls = _366.className;
        if (typeof (cls) != "string") {
            cls = _366.getAttribute("class");
        }
        if (typeof (cls) == "string") {
            var _368 = cls.split(" ");
            for (var j = 0; j < _368.length; j++) {
                if (_368[j] == _360) {
                    _364.push(_366);
                    break;
                }
            }
        }
    }
    return _364;
}, _newCallStack: function(path, once) {
    var rval = function() {
        var _36d = arguments.callee.callStack;
        for (var i = 0; i < _36d.length; i++) {
            if (_36d[i].apply(this, arguments) === false) {
                break;
            }
        }
        if (once) {
            try {
                this[path] = null;
            }
            catch (e) {
            }
        }
    };
    rval.callStack = [];
    return rval;
}, addToCallStack: function(_36f, path, func, once) {
    var self = MochiKit.DOM;
    var _374 = _36f[path];
    var _375 = _374;
    if (!(typeof (_374) == "function" && typeof (_374.callStack) == "object" && _374.callStack !== null)) {
        _375 = self._newCallStack(path, once);
        if (typeof (_374) == "function") {
            _375.callStack.push(_374);
        }
        _36f[path] = _375;
    }
    _375.callStack.push(func);
}, addLoadEvent: function(func) {
    var self = MochiKit.DOM;
    self.addToCallStack(self._window, "onload", func, true);
}, focusOnLoad: function(_378) {
    var self = MochiKit.DOM;
    self.addLoadEvent(function() {
        _378 = self.getElement(_378);
        if (_378) {
            _378.focus();
        }
    });
}, setElementClass: function(_37a, _37b) {
    var self = MochiKit.DOM;
    var obj = self.getElement(_37a);
    if (self.attributeArray.compliant) {
        obj.setAttribute("class", _37b);
    } else {
        obj.setAttribute("className", _37b);
    }
}, toggleElementClass: function(_37e) {
    var self = MochiKit.DOM;
    for (var i = 1; i < arguments.length; i++) {
        var obj = self.getElement(arguments[i]);
        if (!self.addElementClass(obj, _37e)) {
            self.removeElementClass(obj, _37e);
        }
    }
}, addElementClass: function(_382, _383) {
    var self = MochiKit.DOM;
    var obj = self.getElement(_382);
    var cls = obj.className;
    if (typeof (cls) != "string") {
        cls = obj.getAttribute("class");
    }
    if (typeof (cls) != "string" || cls.length === 0) {
        self.setElementClass(obj, _383);
        return true;
    }
    if (cls == _383) {
        return false;
    }
    var _387 = cls.split(" ");
    for (var i = 0; i < _387.length; i++) {
        if (_387[i] == _383) {
            return false;
        }
    }
    self.setElementClass(obj, cls + " " + _383);
    return true;
}, removeElementClass: function(_389, _38a) {
    var self = MochiKit.DOM;
    var obj = self.getElement(_389);
    var cls = obj.className;
    if (typeof (cls) != "string") {
        cls = obj.getAttribute("class");
    }
    if (typeof (cls) != "string" || cls.length === 0) {
        return false;
    }
    if (cls == _38a) {
        self.setElementClass(obj, "");
        return true;
    }
    var _38e = cls.split(" ");
    for (var i = 0; i < _38e.length; i++) {
        if (_38e[i] == _38a) {
            _38e.splice(i, 1);
            self.setElementClass(obj, _38e.join(" "));
            return true;
        }
    }
    return false;
}, swapElementClass: function(_390, _391, _392) {
    var obj = MochiKit.DOM.getElement(_390);
    var res = MochiKit.DOM.removeElementClass(obj, _391);
    if (res) {
        MochiKit.DOM.addElementClass(obj, _392);
    }
    return res;
}, hasElementClass: function(_395, _396) {
    var obj = MochiKit.DOM.getElement(_395);
    if (obj == null) {
        return false;
    }
    var cls = obj.className;
    if (typeof (cls) != "string") {
        cls = obj.getAttribute("class");
    }
    if (typeof (cls) != "string") {
        return false;
    }
    var _399 = cls.split(" ");
    for (var i = 1; i < arguments.length; i++) {
        var good = false;
        for (var j = 0; j < _399.length; j++) {
            if (_399[j] == arguments[i]) {
                good = true;
                break;
            }
        }
        if (!good) {
            return false;
        }
    }
    return true;
}, escapeHTML: function(s) {
    return s.replace(/&/g, "&amp;").replace(/"/g, "&quot;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
}, toHTML: function(dom) {
    return MochiKit.DOM.emitHTML(dom).join("");
}, emitHTML: function(dom, lst) {
    if (typeof (lst) == "undefined" || lst === null) {
        lst = [];
    }
    var _3a1 = [dom];
    var self = MochiKit.DOM;
    var _3a3 = self.escapeHTML;
    var _3a4 = self.attributeArray;
    while (_3a1.length) {
        dom = _3a1.pop();
        if (typeof (dom) == "string") {
            lst.push(dom);
        } else {
            if (dom.nodeType == 1) {
                lst.push("<" + dom.tagName.toLowerCase());
                var _3a5 = [];
                var _3a6 = _3a4(dom);
                for (var i = 0; i < _3a6.length; i++) {
                    var a = _3a6[i];
                    _3a5.push([" ", a.name, "=\"", _3a3(a.value), "\""]);
                }
                _3a5.sort();
                for (i = 0; i < _3a5.length; i++) {
                    var _3a9 = _3a5[i];
                    for (var j = 0; j < _3a9.length; j++) {
                        lst.push(_3a9[j]);
                    }
                }
                if (dom.hasChildNodes()) {
                    lst.push(">");
                    _3a1.push("</" + dom.tagName.toLowerCase() + ">");
                    var _3ab = dom.childNodes;
                    for (i = _3ab.length - 1; i >= 0; i--) {
                        _3a1.push(_3ab[i]);
                    }
                } else {
                    lst.push("/>");
                }
            } else {
                if (dom.nodeType == 3) {
                    lst.push(_3a3(dom.nodeValue));
                }
            }
        }
    }
    return lst;
}, scrapeText: function(node, _3ad) {
    var rval = [];
    (function(node) {
        var cn = node.childNodes;
        if (cn) {
            for (var i = 0; i < cn.length; i++) {
                arguments.callee.call(this, cn[i]);
            }
        }
        var _3b2 = node.nodeValue;
        if (typeof (_3b2) == "string") {
            rval.push(_3b2);
        }
    })(MochiKit.DOM.getElement(node));
    if (_3ad) {
        return rval;
    } else {
        return rval.join("");
    }
}, removeEmptyTextNodes: function(_3b3) {
    _3b3 = MochiKit.DOM.getElement(_3b3);
    for (var i = 0; i < _3b3.childNodes.length; i++) {
        var node = _3b3.childNodes[i];
        if (node.nodeType == 3 && !/\S/.test(node.nodeValue)) {
            node.parentNode.removeChild(node);
        }
    }
}, getFirstElementByTagAndClassName: function(_3b6, _3b7, _3b8) {
    var self = MochiKit.DOM;
    if (typeof (_3b6) == "undefined" || _3b6 === null) {
        _3b6 = "*";
    }
    if (typeof (_3b8) == "undefined" || _3b8 === null) {
        _3b8 = self._document;
    }
    _3b8 = self.getElement(_3b8);
    if (_3b8 == null) {
        return null;
    }
    var _3ba = (_3b8.getElementsByTagName(_3b6) || self._document.all);
    if (_3ba.length <= 0) {
        return null;
    } else {
        if (typeof (_3b7) == "undefined" || _3b7 === null) {
            return _3ba[0];
        }
    }
    for (var i = 0; i < _3ba.length; i++) {
        var _3bc = _3ba[i];
        var cls = _3bc.className;
        if (typeof (cls) != "string") {
            cls = _3bc.getAttribute("class");
        }
        if (typeof (cls) == "string") {
            var _3be = cls.split(" ");
            for (var j = 0; j < _3be.length; j++) {
                if (_3be[j] == _3b7) {
                    return _3bc;
                }
            }
        }
    }
    return null;
}, getFirstParentByTagAndClassName: function(elem, _3c1, _3c2) {
    var self = MochiKit.DOM;
    elem = self.getElement(elem);
    if (typeof (_3c1) == "undefined" || _3c1 === null) {
        _3c1 = "*";
    } else {
        _3c1 = _3c1.toUpperCase();
    }
    if (typeof (_3c2) == "undefined" || _3c2 === null) {
        _3c2 = null;
    }
    if (elem) {
        elem = elem.parentNode;
    }
    while (elem && elem.tagName) {
        var _3c4 = elem.tagName.toUpperCase();
        if ((_3c1 === "*" || _3c1 == _3c4) && (_3c2 === null || self.hasElementClass(elem, _3c2))) {
            return elem;
        }
        elem = elem.parentNode;
    }
    return null;
}, __new__: function(win) {
    var m = MochiKit.Base;
    if (typeof (document) != "undefined") {
        this._document = document;
        var _3c7 = "http://www.mozilla.org/keymaster/gatekeeper/there.is.only.xul";
        this._xhtml = (document.documentElement && document.createElementNS && document.documentElement.namespaceURI === _3c7);
    } else {
        if (MochiKit.MockDOM) {
            this._document = MochiKit.MockDOM.document;
        }
    }
    this._window = win;
    this.domConverters = new m.AdapterRegistry();
    var _3c8 = this._document.createElement("span");
    var _3c9;
    if (_3c8 && _3c8.attributes && _3c8.attributes.length > 0) {
        var _3ca = m.filter;
        _3c9 = function(node) {
            return _3ca(_3c9.ignoreAttrFilter, node.attributes);
        };
        _3c9.ignoreAttr = {};
        var _3cc = _3c8.attributes;
        var _3cd = _3c9.ignoreAttr;
        for (var i = 0; i < _3cc.length; i++) {
            var a = _3cc[i];
            _3cd[a.name] = a.value;
        }
        _3c9.ignoreAttrFilter = function(a) {
            return (_3c9.ignoreAttr[a.name] != a.value);
        };
        _3c9.compliant = false;
        _3c9.renames = { "class": "className", "checked": "defaultChecked", "usemap": "useMap", "for": "htmlFor", "readonly": "readOnly", "colspan": "colSpan", "bgcolor": "bgColor", "cellspacing": "cellSpacing", "cellpadding": "cellPadding" };
    } else {
        _3c9 = function(node) {
            return node.attributes;
        };
        _3c9.compliant = true;
        _3c9.ignoreAttr = {};
        _3c9.renames = {};
    }
    this.attributeArray = _3c9;
    var _3d2 = function(_3d3, arr) {
        var _3d5 = arr[0];
        var _3d6 = arr[1];
        var _3d7 = _3d6.split(".")[1];
        var str = "";
        str += "if (!MochiKit." + _3d7 + ") { throw new Error(\"";
        str += "This function has been deprecated and depends on MochiKit.";
        str += _3d7 + ".\");}";
        str += "return " + _3d6 + ".apply(this, arguments);";
        MochiKit[_3d3][_3d5] = new Function(str);
    };
    for (var i = 0; i < MochiKit.DOM.DEPRECATED.length; i++) {
        _3d2("DOM", MochiKit.DOM.DEPRECATED[i]);
    }
    var _3d9 = this.createDOMFunc;
    this.UL = _3d9("ul");
    this.OL = _3d9("ol");
    this.LI = _3d9("li");
    this.DL = _3d9("dl");
    this.DT = _3d9("dt");
    this.DD = _3d9("dd");
    this.TD = _3d9("td");
    this.TR = _3d9("tr");
    this.TBODY = _3d9("tbody");
    this.THEAD = _3d9("thead");
    this.TFOOT = _3d9("tfoot");
    this.TABLE = _3d9("table");
    this.TH = _3d9("th");
    this.INPUT = _3d9("input");
    this.SPAN = _3d9("span");
    this.A = _3d9("a");
    this.DIV = _3d9("div");
    this.IMG = _3d9("img");
    this.BUTTON = _3d9("button");
    this.TT = _3d9("tt");
    this.PRE = _3d9("pre");
    this.H1 = _3d9("h1");
    this.H2 = _3d9("h2");
    this.H3 = _3d9("h3");
    this.H4 = _3d9("h4");
    this.H5 = _3d9("h5");
    this.H6 = _3d9("h6");
    this.BR = _3d9("br");
    this.HR = _3d9("hr");
    this.LABEL = _3d9("label");
    this.TEXTAREA = _3d9("textarea");
    this.FORM = _3d9("form");
    this.P = _3d9("p");
    this.SELECT = _3d9("select");
    this.OPTION = _3d9("option");
    this.OPTGROUP = _3d9("optgroup");
    this.LEGEND = _3d9("legend");
    this.FIELDSET = _3d9("fieldset");
    this.STRONG = _3d9("strong");
    this.CANVAS = _3d9("canvas");
    this.$ = this.getElement;
    this.EXPORT_TAGS = { ":common": this.EXPORT, ":all": m.concat(this.EXPORT, this.EXPORT_OK) };
    m.nameFunctions(this);
} 
});
MochiKit.DOM.__new__(((typeof (window) == "undefined") ? this : window));
if (MochiKit.__export__) {
    withWindow = MochiKit.DOM.withWindow;
    withDocument = MochiKit.DOM.withDocument;
}
MochiKit.Base._exportSymbols(this, MochiKit.DOM);
MochiKit.Base._deps("Selector", ["Base", "DOM", "Iter"]);
MochiKit.Selector.NAME = "MochiKit.Selector";
MochiKit.Selector.VERSION = "1.4.2";
MochiKit.Selector.__repr__ = function() {
    return "[" + this.NAME + " " + this.VERSION + "]";
};
MochiKit.Selector.toString = function() {
    return this.__repr__();
};
MochiKit.Selector.EXPORT = ["Selector", "findChildElements", "findDocElements", "$$"];
MochiKit.Selector.EXPORT_OK = [];
MochiKit.Selector.Selector = function(_3da) {
    this.params = { classNames: [], pseudoClassNames: [] };
    this.expression = _3da.toString().replace(/(^\s+|\s+$)/g, "");
    this.parseExpression();
    this.compileMatcher();
};
MochiKit.Selector.Selector.prototype = { __class__: MochiKit.Selector.Selector, parseExpression: function() {
    function abort(_3db) {
        throw "Parse error in selector: " + _3db;
    }
    if (this.expression == "") {
        abort("empty expression");
    }
    var repr = MochiKit.Base.repr;
    var _3dd = this.params;
    var expr = this.expression;
    var _3df, _3e0, _3e1, rest;
    while (_3df = expr.match(/^(.*)\[([a-z0-9_:-]+?)(?:([~\|!^$*]?=)(?:"([^"]*)"|([^\]\s]*)))?\]$/i)) {
        _3dd.attributes = _3dd.attributes || [];
        _3dd.attributes.push({ name: _3df[2], operator: _3df[3], value: _3df[4] || _3df[5] || "" });
        expr = _3df[1];
    }
    if (expr == "*") {
        return this.params.wildcard = true;
    }
    while (_3df = expr.match(/^([^a-z0-9_-])?([a-z0-9_-]+(?:\([^)]*\))?)(.*)/i)) {
        _3e0 = _3df[1];
        _3e1 = _3df[2];
        rest = _3df[3];
        switch (_3e0) {
            case "#":
                _3dd.id = _3e1;
                break;
            case ".":
                _3dd.classNames.push(_3e1);
                break;
            case ":":
                _3dd.pseudoClassNames.push(_3e1);
                break;
            case "":
            case undefined:
                _3dd.tagName = _3e1.toUpperCase();
                break;
            default:
                abort(repr(expr));
        }
        expr = rest;
    }
    if (expr.length > 0) {
        abort(repr(expr));
    }
}, buildMatchExpression: function() {
    var repr = MochiKit.Base.repr;
    var _3e4 = this.params;
    var _3e5 = [];
    var _3e6, i;
    function childElements(_3e8) {
        return "MochiKit.Base.filter(function (node) { return node.nodeType == 1; }, " + _3e8 + ".childNodes)";
    }
    if (_3e4.wildcard) {
        _3e5.push("true");
    }
    if (_3e6 = _3e4.id) {
        _3e5.push("element.id == " + repr(_3e6));
    }
    if (_3e6 = _3e4.tagName) {
        _3e5.push("element.tagName.toUpperCase() == " + repr(_3e6));
    }
    if ((_3e6 = _3e4.classNames).length > 0) {
        for (i = 0; i < _3e6.length; i++) {
            _3e5.push("MochiKit.DOM.hasElementClass(element, " + repr(_3e6[i]) + ")");
        }
    }
    if ((_3e6 = _3e4.pseudoClassNames).length > 0) {
        for (i = 0; i < _3e6.length; i++) {
            var _3e9 = _3e6[i].match(/^([^(]+)(?:\((.*)\))?$/);
            var _3ea = _3e9[1];
            var _3eb = _3e9[2];
            switch (_3ea) {
                case "root":
                    _3e5.push("element.nodeType == 9 || element === element.ownerDocument.documentElement");
                    break;
                case "nth-child":
                case "nth-last-child":
                case "nth-of-type":
                case "nth-last-of-type":
                    _3e9 = _3eb.match(/^((?:(\d+)n\+)?(\d+)|odd|even)$/);
                    if (!_3e9) {
                        throw "Invalid argument to pseudo element nth-child: " + _3eb;
                    }
                    var a, b;
                    if (_3e9[0] == "odd") {
                        a = 2;
                        b = 1;
                    } else {
                        if (_3e9[0] == "even") {
                            a = 2;
                            b = 0;
                        } else {
                            a = _3e9[2] && parseInt(_3e9) || null;
                            b = parseInt(_3e9[3]);
                        }
                    }
                    _3e5.push("this.nthChild(element," + a + "," + b + "," + !!_3ea.match("^nth-last") + "," + !!_3ea.match("of-type$") + ")");
                    break;
                case "first-child":
                    _3e5.push("this.nthChild(element, null, 1)");
                    break;
                case "last-child":
                    _3e5.push("this.nthChild(element, null, 1, true)");
                    break;
                case "first-of-type":
                    _3e5.push("this.nthChild(element, null, 1, false, true)");
                    break;
                case "last-of-type":
                    _3e5.push("this.nthChild(element, null, 1, true, true)");
                    break;
                case "only-child":
                    _3e5.push(childElements("element.parentNode") + ".length == 1");
                    break;
                case "only-of-type":
                    _3e5.push("MochiKit.Base.filter(function (node) { return node.tagName == element.tagName; }, " + childElements("element.parentNode") + ").length == 1");
                    break;
                case "empty":
                    _3e5.push("element.childNodes.length == 0");
                    break;
                case "enabled":
                    _3e5.push("(this.isUIElement(element) && element.disabled === false)");
                    break;
                case "disabled":
                    _3e5.push("(this.isUIElement(element) && element.disabled === true)");
                    break;
                case "checked":
                    _3e5.push("(this.isUIElement(element) && element.checked === true)");
                    break;
                case "not":
                    var _3ee = new MochiKit.Selector.Selector(_3eb);
                    _3e5.push("!( " + _3ee.buildMatchExpression() + ")");
                    break;
            }
        }
    }
    if (_3e6 = _3e4.attributes) {
        MochiKit.Base.map(function(_3ef) {
            var _3f0 = "MochiKit.DOM.getNodeAttribute(element, " + repr(_3ef.name) + ")";
            var _3f1 = function(_3f2) {
                return _3f0 + ".split(" + repr(_3f2) + ")";
            };
            _3e5.push(_3f0 + " != null");
            switch (_3ef.operator) {
                case "=":
                    _3e5.push(_3f0 + " == " + repr(_3ef.value));
                    break;
                case "~=":
                    _3e5.push("MochiKit.Base.findValue(" + _3f1(" ") + ", " + repr(_3ef.value) + ") > -1");
                    break;
                case "^=":
                    _3e5.push(_3f0 + ".substring(0, " + _3ef.value.length + ") == " + repr(_3ef.value));
                    break;
                case "$=":
                    _3e5.push(_3f0 + ".substring(" + _3f0 + ".length - " + _3ef.value.length + ") == " + repr(_3ef.value));
                    break;
                case "*=":
                    _3e5.push(_3f0 + ".match(" + repr(_3ef.value) + ")");
                    break;
                case "|=":
                    _3e5.push(_3f1("-") + "[0].toUpperCase() == " + repr(_3ef.value.toUpperCase()));
                    break;
                case "!=":
                    _3e5.push(_3f0 + " != " + repr(_3ef.value));
                    break;
                case "":
                case undefined:
                    break;
                default:
                    throw "Unknown operator " + _3ef.operator + " in selector";
            }
        }, _3e6);
    }
    return _3e5.join(" && ");
}, compileMatcher: function() {
    var code = "return (!element.tagName) ? false : " + this.buildMatchExpression() + ";";
    this.match = new Function("element", code);
}, nthChild: function(_3f4, a, b, _3f7, _3f8) {
    var _3f9 = MochiKit.Base.filter(function(node) {
        return node.nodeType == 1;
    }, _3f4.parentNode.childNodes);
    if (_3f8) {
        _3f9 = MochiKit.Base.filter(function(node) {
            return node.tagName == _3f4.tagName;
        }, _3f9);
    }
    if (_3f7) {
        _3f9 = MochiKit.Iter.reversed(_3f9);
    }
    if (a) {
        var _3fc = MochiKit.Base.findIdentical(_3f9, _3f4);
        return ((_3fc + 1 - b) / a) % 1 == 0;
    } else {
        return b == MochiKit.Base.findIdentical(_3f9, _3f4) + 1;
    }
}, isUIElement: function(_3fd) {
    return MochiKit.Base.findValue(["input", "button", "select", "option", "textarea", "object"], _3fd.tagName.toLowerCase()) > -1;
}, findElements: function(_3fe, axis) {
    var _400;
    if (axis == undefined) {
        axis = "";
    }
    function inScope(_401, _402) {
        if (axis == "") {
            return MochiKit.DOM.isChildNode(_401, _402);
        } else {
            if (axis == ">") {
                return _401.parentNode === _402;
            } else {
                if (axis == "+") {
                    return _401 === nextSiblingElement(_402);
                } else {
                    if (axis == "~") {
                        var _403 = _402;
                        while (_403 = nextSiblingElement(_403)) {
                            if (_401 === _403) {
                                return true;
                            }
                        }
                        return false;
                    } else {
                        throw "Invalid axis: " + axis;
                    }
                }
            }
        }
    }
    if (_400 = MochiKit.DOM.getElement(this.params.id)) {
        if (this.match(_400)) {
            if (!_3fe || inScope(_400, _3fe)) {
                return [_400];
            }
        }
    }
    function nextSiblingElement(node) {
        node = node.nextSibling;
        while (node && node.nodeType != 1) {
            node = node.nextSibling;
        }
        return node;
    }
    if (axis == "") {
        _3fe = (_3fe || MochiKit.DOM.currentDocument()).getElementsByTagName(this.params.tagName || "*");
    } else {
        if (axis == ">") {
            if (!_3fe) {
                throw "> combinator not allowed without preceeding expression";
            }
            _3fe = MochiKit.Base.filter(function(node) {
                return node.nodeType == 1;
            }, _3fe.childNodes);
        } else {
            if (axis == "+") {
                if (!_3fe) {
                    throw "+ combinator not allowed without preceeding expression";
                }
                _3fe = nextSiblingElement(_3fe) && [nextSiblingElement(_3fe)];
            } else {
                if (axis == "~") {
                    if (!_3fe) {
                        throw "~ combinator not allowed without preceeding expression";
                    }
                    var _406 = [];
                    while (nextSiblingElement(_3fe)) {
                        _3fe = nextSiblingElement(_3fe);
                        _406.push(_3fe);
                    }
                    _3fe = _406;
                }
            }
        }
    }
    if (!_3fe) {
        return [];
    }
    var _407 = MochiKit.Base.filter(MochiKit.Base.bind(function(_408) {
        return this.match(_408);
    }, this), _3fe);
    return _407;
}, repr: function() {
    return "Selector(" + this.expression + ")";
}, toString: MochiKit.Base.forwardCall("repr")
};
MochiKit.Base.update(MochiKit.Selector, { findChildElements: function(_409, _40a) {
    var uniq = function(arr) {
        var res = [];
        for (var i = 0; i < arr.length; i++) {
            if (MochiKit.Base.findIdentical(res, arr[i]) < 0) {
                res.push(arr[i]);
            }
        }
        return res;
    };
    return MochiKit.Base.flattenArray(MochiKit.Base.map(function(_40f) {
        var _410 = "";
        var _411 = function(_412, expr) {
            if (match = expr.match(/^[>+~]$/)) {
                _410 = match[0];
                return _412;
            } else {
                var _414 = new MochiKit.Selector.Selector(expr);
                var _415 = MochiKit.Iter.reduce(function(_416, _417) {
                    return MochiKit.Base.extend(_416, _414.findElements(_417 || _409, _410));
                }, _412, []);
                _410 = "";
                return _415;
            }
        };
        var _418 = _40f.replace(/(^\s+|\s+$)/g, "").split(/\s+/);
        return uniq(MochiKit.Iter.reduce(_411, _418, [null]));
    }, _40a));
}, findDocElements: function() {
    return MochiKit.Selector.findChildElements(MochiKit.DOM.currentDocument(), arguments);
}, __new__: function() {
    var m = MochiKit.Base;
    this.$$ = this.findDocElements;
    this.EXPORT_TAGS = { ":common": this.EXPORT, ":all": m.concat(this.EXPORT, this.EXPORT_OK) };
    m.nameFunctions(this);
} 
});
MochiKit.Selector.__new__();
MochiKit.Base._exportSymbols(this, MochiKit.Selector);
MochiKit.Base._deps("Style", ["Base", "DOM"]);
MochiKit.Style.NAME = "MochiKit.Style";
MochiKit.Style.VERSION = "1.4.2";
MochiKit.Style.__repr__ = function() {
    return "[" + this.NAME + " " + this.VERSION + "]";
};
MochiKit.Style.toString = function() {
    return this.__repr__();
};
MochiKit.Style.EXPORT_OK = [];
MochiKit.Style.EXPORT = ["setStyle", "setOpacity", "getStyle", "getElementDimensions", "elementDimensions", "setElementDimensions", "getElementPosition", "elementPosition", "setElementPosition", "makePositioned", "undoPositioned", "makeClipping", "undoClipping", "setDisplayForElement", "hideElement", "showElement", "getViewportDimensions", "getViewportPosition", "Dimensions", "Coordinates"];
MochiKit.Style.Dimensions = function(w, h) {
    this.w = w;
    this.h = h;
};
MochiKit.Style.Dimensions.prototype.__repr__ = function() {
    var repr = MochiKit.Base.repr;
    return "{w: " + repr(this.w) + ", h: " + repr(this.h) + "}";
};
MochiKit.Style.Dimensions.prototype.toString = function() {
    return this.__repr__();
};
MochiKit.Style.Coordinates = function(x, y) {
    this.x = x;
    this.y = y;
};
MochiKit.Style.Coordinates.prototype.__repr__ = function() {
    var repr = MochiKit.Base.repr;
    return "{x: " + repr(this.x) + ", y: " + repr(this.y) + "}";
};
MochiKit.Style.Coordinates.prototype.toString = function() {
    return this.__repr__();
};
MochiKit.Base.update(MochiKit.Style, { getStyle: function(elem, _421) {
    var dom = MochiKit.DOM;
    var d = dom._document;
    elem = dom.getElement(elem);
    _421 = MochiKit.Base.camelize(_421);
    if (!elem || elem == d) {
        return undefined;
    }
    if (_421 == "opacity" && typeof (elem.filters) != "undefined") {
        var _424 = (MochiKit.Style.getStyle(elem, "filter") || "").match(/alpha\(opacity=(.*)\)/);
        if (_424 && _424[1]) {
            return parseFloat(_424[1]) / 100;
        }
        return 1;
    }
    if (_421 == "float" || _421 == "cssFloat" || _421 == "styleFloat") {
        if (elem.style["float"]) {
            return elem.style["float"];
        } else {
            if (elem.style.cssFloat) {
                return elem.style.cssFloat;
            } else {
                if (elem.style.styleFloat) {
                    return elem.style.styleFloat;
                } else {
                    return "none";
                }
            }
        }
    }
    var _425 = elem.style ? elem.style[_421] : null;
    if (!_425) {
        if (d.defaultView && d.defaultView.getComputedStyle) {
            var css = d.defaultView.getComputedStyle(elem, null);
            _421 = _421.replace(/([A-Z])/g, "-$1").toLowerCase();
            _425 = css ? css.getPropertyValue(_421) : null;
        } else {
            if (elem.currentStyle) {
                _425 = elem.currentStyle[_421];
                if (/^\d/.test(_425) && !/px$/.test(_425) && _421 != "fontWeight") {
                    var left = elem.style.left;
                    var _428 = elem.runtimeStyle.left;
                    elem.runtimeStyle.left = elem.currentStyle.left;
                    elem.style.left = _425 || 0;
                    _425 = elem.style.pixelLeft + "px";
                    elem.style.left = left;
                    elem.runtimeStyle.left = _428;
                }
            }
        }
    }
    if (_421 == "opacity") {
        _425 = parseFloat(_425);
    }
    if (/Opera/.test(navigator.userAgent) && (MochiKit.Base.findValue(["left", "top", "right", "bottom"], _421) != -1)) {
        if (MochiKit.Style.getStyle(elem, "position") == "static") {
            _425 = "auto";
        }
    }
    return _425 == "auto" ? null : _425;
}, setStyle: function(elem, _42a) {
    elem = MochiKit.DOM.getElement(elem);
    for (var name in _42a) {
        switch (name) {
            case "opacity":
                MochiKit.Style.setOpacity(elem, _42a[name]);
                break;
            case "float":
            case "cssFloat":
            case "styleFloat":
                if (typeof (elem.style["float"]) != "undefined") {
                    elem.style["float"] = _42a[name];
                } else {
                    if (typeof (elem.style.cssFloat) != "undefined") {
                        elem.style.cssFloat = _42a[name];
                    } else {
                        elem.style.styleFloat = _42a[name];
                    }
                }
                break;
            default:
                elem.style[MochiKit.Base.camelize(name)] = _42a[name];
        }
    }
}, setOpacity: function(elem, o) {
    elem = MochiKit.DOM.getElement(elem);
    var self = MochiKit.Style;
    if (o == 1) {
        var _42f = /Gecko/.test(navigator.userAgent) && !(/Konqueror|AppleWebKit|KHTML/.test(navigator.userAgent));
        elem.style["opacity"] = _42f ? 0.999999 : 1;
        if (/MSIE/.test(navigator.userAgent)) {
            elem.style["filter"] = self.getStyle(elem, "filter").replace(/alpha\([^\)]*\)/gi, "");
        }
    } else {
        if (o < 0.00001) {
            o = 0;
        }
        elem.style["opacity"] = o;
        if (/MSIE/.test(navigator.userAgent)) {
            elem.style["filter"] = self.getStyle(elem, "filter").replace(/alpha\([^\)]*\)/gi, "") + "alpha(opacity=" + o * 100 + ")";
        }
    }
}, getElementPosition: function(elem, _431) {
    var self = MochiKit.Style;
    var dom = MochiKit.DOM;
    elem = dom.getElement(elem);
    if (!elem || (!(elem.x && elem.y) && (!elem.parentNode === null || self.getStyle(elem, "display") == "none"))) {
        return undefined;
    }
    var c = new self.Coordinates(0, 0);
    var box = null;
    var _436 = null;
    var d = MochiKit.DOM._document;
    var de = d.documentElement;
    var b = d.body;
    if (!elem.parentNode && elem.x && elem.y) {
        c.x += elem.x || 0;
        c.y += elem.y || 0;
    } else {
        if (elem.getBoundingClientRect) {
            box = elem.getBoundingClientRect();
            c.x += box.left + (de.scrollLeft || b.scrollLeft) - (de.clientLeft || 0);
            c.y += box.top + (de.scrollTop || b.scrollTop) - (de.clientTop || 0);
        } else {
            if (elem.offsetParent) {
                c.x += elem.offsetLeft;
                c.y += elem.offsetTop;
                _436 = elem.offsetParent;
                if (_436 != elem) {
                    while (_436) {
                        c.x += parseInt(_436.style.borderLeftWidth) || 0;
                        c.y += parseInt(_436.style.borderTopWidth) || 0;
                        c.x += _436.offsetLeft;
                        c.y += _436.offsetTop;
                        _436 = _436.offsetParent;
                    }
                }
                var ua = navigator.userAgent.toLowerCase();
                if ((typeof (opera) != "undefined" && parseFloat(opera.version()) < 9) || (ua.indexOf("AppleWebKit") != -1 && self.getStyle(elem, "position") == "absolute")) {
                    c.x -= b.offsetLeft;
                    c.y -= b.offsetTop;
                }
                if (elem.parentNode) {
                    _436 = elem.parentNode;
                } else {
                    _436 = null;
                }
                while (_436) {
                    var _43b = _436.tagName.toUpperCase();
                    if (_43b === "BODY" || _43b === "HTML") {
                        break;
                    }
                    var disp = self.getStyle(_436, "display");
                    if (disp.search(/^inline|table-row.*$/i)) {
                        c.x -= _436.scrollLeft;
                        c.y -= _436.scrollTop;
                    }
                    if (_436.parentNode) {
                        _436 = _436.parentNode;
                    } else {
                        _436 = null;
                    }
                }
            }
        }
    }
    if (typeof (_431) != "undefined") {
        _431 = arguments.callee(_431);
        if (_431) {
            c.x -= (_431.x || 0);
            c.y -= (_431.y || 0);
        }
    }
    return c;
}, setElementPosition: function(elem, _43e, _43f) {
    elem = MochiKit.DOM.getElement(elem);
    if (typeof (_43f) == "undefined") {
        _43f = "px";
    }
    var _440 = {};
    var _441 = MochiKit.Base.isUndefinedOrNull;
    if (!_441(_43e.x)) {
        _440["left"] = _43e.x + _43f;
    }
    if (!_441(_43e.y)) {
        _440["top"] = _43e.y + _43f;
    }
    MochiKit.DOM.updateNodeAttributes(elem, { "style": _440 });
}, makePositioned: function(_442) {
    _442 = MochiKit.DOM.getElement(_442);
    var pos = MochiKit.Style.getStyle(_442, "position");
    if (pos == "static" || !pos) {
        _442.style.position = "relative";
        if (/Opera/.test(navigator.userAgent)) {
            _442.style.top = 0;
            _442.style.left = 0;
        }
    }
}, undoPositioned: function(_444) {
    _444 = MochiKit.DOM.getElement(_444);
    if (_444.style.position == "relative") {
        _444.style.position = _444.style.top = _444.style.left = _444.style.bottom = _444.style.right = "";
    }
}, makeClipping: function(_445) {
    _445 = MochiKit.DOM.getElement(_445);
    var s = _445.style;
    var _447 = { "overflow": s.overflow, "overflow-x": s.overflowX, "overflow-y": s.overflowY };
    if ((MochiKit.Style.getStyle(_445, "overflow") || "visible") != "hidden") {
        _445.style.overflow = "hidden";
        _445.style.overflowX = "hidden";
        _445.style.overflowY = "hidden";
    }
    return _447;
}, undoClipping: function(_448, _449) {
    _448 = MochiKit.DOM.getElement(_448);
    if (typeof (_449) == "string") {
        _448.style.overflow = _449;
    } else {
        if (_449 != null) {
            _448.style.overflow = _449["overflow"];
            _448.style.overflowX = _449["overflow-x"];
            _448.style.overflowY = _449["overflow-y"];
        }
    }
}, getElementDimensions: function(elem, _44b) {
    var self = MochiKit.Style;
    var dom = MochiKit.DOM;
    if (typeof (elem.w) == "number" || typeof (elem.h) == "number") {
        return new self.Dimensions(elem.w || 0, elem.h || 0);
    }
    elem = dom.getElement(elem);
    if (!elem) {
        return undefined;
    }
    var disp = self.getStyle(elem, "display");
    if (disp == "none" || disp == "" || typeof (disp) == "undefined") {
        var s = elem.style;
        var _450 = s.visibility;
        var _451 = s.position;
        var _452 = s.display;
        s.visibility = "hidden";
        s.position = "absolute";
        s.display = self._getDefaultDisplay(elem);
        var _453 = elem.offsetWidth;
        var _454 = elem.offsetHeight;
        s.display = _452;
        s.position = _451;
        s.visibility = _450;
    } else {
        _453 = elem.offsetWidth || 0;
        _454 = elem.offsetHeight || 0;
    }
    if (_44b) {
        var _455 = "colSpan" in elem && "rowSpan" in elem;
        var _456 = (_455 && elem.parentNode && self.getStyle(elem.parentNode, "borderCollapse") == "collapse");
        if (_456) {
            if (/MSIE/.test(navigator.userAgent)) {
                var _457 = elem.previousSibling ? 0.5 : 1;
                var _458 = elem.nextSibling ? 0.5 : 1;
            } else {
                var _457 = 0.5;
                var _458 = 0.5;
            }
        } else {
            var _457 = 1;
            var _458 = 1;
        }
        _453 -= Math.round((parseFloat(self.getStyle(elem, "paddingLeft")) || 0) + (parseFloat(self.getStyle(elem, "paddingRight")) || 0) + _457 * (parseFloat(self.getStyle(elem, "borderLeftWidth")) || 0) + _458 * (parseFloat(self.getStyle(elem, "borderRightWidth")) || 0));
        if (_455) {
            if (/Gecko|Opera/.test(navigator.userAgent) && !/Konqueror|AppleWebKit|KHTML/.test(navigator.userAgent)) {
                var _459 = 0;
            } else {
                if (/MSIE/.test(navigator.userAgent)) {
                    var _459 = 1;
                } else {
                    var _459 = _456 ? 0.5 : 1;
                }
            }
        } else {
            var _459 = 1;
        }
        _454 -= Math.round((parseFloat(self.getStyle(elem, "paddingTop")) || 0) + (parseFloat(self.getStyle(elem, "paddingBottom")) || 0) + _459 * ((parseFloat(self.getStyle(elem, "borderTopWidth")) || 0) + (parseFloat(self.getStyle(elem, "borderBottomWidth")) || 0)));
    }
    return new self.Dimensions(_453, _454);
}, setElementDimensions: function(elem, _45b, _45c) {
    elem = MochiKit.DOM.getElement(elem);
    if (typeof (_45c) == "undefined") {
        _45c = "px";
    }
    var _45d = {};
    var _45e = MochiKit.Base.isUndefinedOrNull;
    if (!_45e(_45b.w)) {
        _45d["width"] = _45b.w + _45c;
    }
    if (!_45e(_45b.h)) {
        _45d["height"] = _45b.h + _45c;
    }
    MochiKit.DOM.updateNodeAttributes(elem, { "style": _45d });
}, _getDefaultDisplay: function(elem) {
    var self = MochiKit.Style;
    var dom = MochiKit.DOM;
    elem = dom.getElement(elem);
    if (!elem) {
        return undefined;
    }
    var _462 = elem.tagName.toUpperCase();
    return self._defaultDisplay[_462] || "block";
}, setDisplayForElement: function(_463, _464) {
    var _465 = MochiKit.Base.extend(null, arguments, 1);
    var _466 = MochiKit.DOM.getElement;
    for (var i = 0; i < _465.length; i++) {
        _464 = _466(_465[i]);
        if (_464) {
            _464.style.display = _463;
        }
    }
}, getViewportDimensions: function() {
    var d = new MochiKit.Style.Dimensions();
    var w = MochiKit.DOM._window;
    var b = MochiKit.DOM._document.body;
    if (w.innerWidth) {
        d.w = w.innerWidth;
        d.h = w.innerHeight;
    } else {
        if (b && b.parentElement && b.parentElement.clientWidth) {
            d.w = b.parentElement.clientWidth;
            d.h = b.parentElement.clientHeight;
        } else {
            if (b && b.clientWidth) {
                d.w = b.clientWidth;
                d.h = b.clientHeight;
            }
        }
    }
    return d;
}, getViewportPosition: function() {
    var c = new MochiKit.Style.Coordinates(0, 0);
    var d = MochiKit.DOM._document;
    var de = d.documentElement;
    var db = d.body;
    if (de && (de.scrollTop || de.scrollLeft)) {
        c.x = de.scrollLeft;
        c.y = de.scrollTop;
    } else {
        if (db) {
            c.x = db.scrollLeft;
            c.y = db.scrollTop;
        }
    }
    return c;
}, __new__: function() {
    var m = MochiKit.Base;
    var _470 = ["A", "ABBR", "ACRONYM", "B", "BASEFONT", "BDO", "BIG", "BR", "CITE", "CODE", "DFN", "EM", "FONT", "I", "IMG", "KBD", "LABEL", "Q", "S", "SAMP", "SMALL", "SPAN", "STRIKE", "STRONG", "SUB", "SUP", "TEXTAREA", "TT", "U", "VAR"];
    this._defaultDisplay = { "TABLE": "table", "THEAD": "table-header-group", "TBODY": "table-row-group", "TFOOT": "table-footer-group", "COLGROUP": "table-column-group", "COL": "table-column", "TR": "table-row", "TD": "table-cell", "TH": "table-cell", "CAPTION": "table-caption", "LI": "list-item", "INPUT": "inline-block", "SELECT": "inline-block" };
    if (/MSIE/.test(navigator.userAgent)) {
        for (var k in this._defaultDisplay) {
            var v = this._defaultDisplay[k];
            if (v.indexOf("table") == 0) {
                this._defaultDisplay[k] = "block";
            }
        }
    }
    for (var i = 0; i < _470.length; i++) {
        this._defaultDisplay[_470[i]] = "inline";
    }
    this.elementPosition = this.getElementPosition;
    this.elementDimensions = this.getElementDimensions;
    this.hideElement = m.partial(this.setDisplayForElement, "none");
    this.showElement = m.partial(this.setDisplayForElement, "block");
    this.EXPORT_TAGS = { ":common": this.EXPORT, ":all": m.concat(this.EXPORT, this.EXPORT_OK) };
    m.nameFunctions(this);
} 
});
MochiKit.Style.__new__();
MochiKit.Base._exportSymbols(this, MochiKit.Style);
MochiKit.Base._deps("LoggingPane", ["Base", "Logging"]);
MochiKit.LoggingPane.NAME = "MochiKit.LoggingPane";
MochiKit.LoggingPane.VERSION = "1.4.2";
MochiKit.LoggingPane.__repr__ = function() {
    return "[" + this.NAME + " " + this.VERSION + "]";
};
MochiKit.LoggingPane.toString = function() {
    return this.__repr__();
};
MochiKit.LoggingPane.createLoggingPane = function(_474) {
    var m = MochiKit.LoggingPane;
    _474 = !(!_474);
    if (m._loggingPane && m._loggingPane.inline != _474) {
        m._loggingPane.closePane();
        m._loggingPane = null;
    }
    if (!m._loggingPane || m._loggingPane.closed) {
        m._loggingPane = new m.LoggingPane(_474, MochiKit.Logging.logger);
    }
    return m._loggingPane;
};
MochiKit.LoggingPane.LoggingPane = function(_476, _477) {
    if (typeof (_477) == "undefined" || _477 === null) {
        _477 = MochiKit.Logging.logger;
    }
    this.logger = _477;
    var _478 = MochiKit.Base.update;
    var _479 = MochiKit.Base.updatetree;
    var bind = MochiKit.Base.bind;
    var _47b = MochiKit.Base.clone;
    var win = window;
    var uid = "_MochiKit_LoggingPane";
    if (typeof (MochiKit.DOM) != "undefined") {
        win = MochiKit.DOM.currentWindow();
    }
    if (!_476) {
        var url = win.location.href.split("?")[0].replace(/[#:\/.><&%-]/g, "_");
        var name = uid + "_" + url;
        var nwin = win.open("", name, "dependent,resizable,height=200");
        if (!nwin) {
            alert("Not able to open debugging window due to pop-up blocking.");
            return undefined;
        }
        nwin.document.write("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\" " + "\"http://www.w3.org/TR/html4/loose.dtd\">" + "<html><head><title>[MochiKit.LoggingPane]</title></head>" + "<body></body></html>");
        nwin.document.close();
        nwin.document.title += " " + win.document.title;
        win = nwin;
    }
    var doc = win.document;
    this.doc = doc;
    var _482 = doc.getElementById(uid);
    var _483 = !!_482;
    if (_482 && typeof (_482.loggingPane) != "undefined") {
        _482.loggingPane.logger = this.logger;
        _482.loggingPane.buildAndApplyFilter();
        return _482.loggingPane;
    }
    if (_483) {
        var _484;
        while ((_484 = _482.firstChild)) {
            _482.removeChild(_484);
        }
    } else {
        _482 = doc.createElement("div");
        _482.id = uid;
    }
    _482.loggingPane = this;
    var _485 = doc.createElement("input");
    var _486 = doc.createElement("input");
    var _487 = doc.createElement("button");
    var _488 = doc.createElement("button");
    var _489 = doc.createElement("button");
    var _48a = doc.createElement("button");
    var _48b = doc.createElement("div");
    var _48c = doc.createElement("div");
    var _48d = uid + "_Listener";
    this.colorTable = _47b(this.colorTable);
    var _48e = [];
    var _48f = null;
    var _490 = function(msg) {
        var _492 = msg.level;
        if (typeof (_492) == "number") {
            _492 = MochiKit.Logging.LogLevel[_492];
        }
        return _492;
    };
    var _493 = function(msg) {
        return msg.info.join(" ");
    };
    var _495 = bind(function(msg) {
        var _497 = _490(msg);
        var text = _493(msg);
        var c = this.colorTable[_497];
        var p = doc.createElement("span");
        p.className = "MochiKit-LogMessage MochiKit-LogLevel-" + _497;
        p.style.cssText = "margin: 0px; white-space: -moz-pre-wrap; white-space: -o-pre-wrap; white-space: pre-wrap; white-space: pre-line; word-wrap: break-word; wrap-option: emergency; color: " + c;
        p.appendChild(doc.createTextNode(_497 + ": " + text));
        _48c.appendChild(p);
        _48c.appendChild(doc.createElement("br"));
        if (_48b.offsetHeight > _48b.scrollHeight) {
            _48b.scrollTop = 0;
        } else {
            _48b.scrollTop = _48b.scrollHeight;
        }
    }, this);
    var _49b = function(msg) {
        _48e[_48e.length] = msg;
        _495(msg);
    };
    var _49d = function() {
        var _49e, _49f;
        try {
            _49e = new RegExp(_485.value);
            _49f = new RegExp(_486.value);
        }
        catch (e) {
            logDebug("Error in filter regex: " + e.message);
            return null;
        }
        return function(msg) {
            return (_49e.test(_490(msg)) && _49f.test(_493(msg)));
        };
    };
    var _4a1 = function() {
        while (_48c.firstChild) {
            _48c.removeChild(_48c.firstChild);
        }
    };
    var _4a2 = function() {
        _48e = [];
        _4a1();
    };
    var _4a3 = bind(function() {
        if (this.closed) {
            return;
        }
        this.closed = true;
        if (MochiKit.LoggingPane._loggingPane == this) {
            MochiKit.LoggingPane._loggingPane = null;
        }
        this.logger.removeListener(_48d);
        try {
            try {
                _482.loggingPane = null;
            }
            catch (e) {
                logFatal("Bookmarklet was closed incorrectly.");
            }
            if (_476) {
                _482.parentNode.removeChild(_482);
            } else {
                this.win.close();
            }
        }
        catch (e) {
        }
    }, this);
    var _4a4 = function() {
        _4a1();
        for (var i = 0; i < _48e.length; i++) {
            var msg = _48e[i];
            if (_48f === null || _48f(msg)) {
                _495(msg);
            }
        }
    };
    this.buildAndApplyFilter = function() {
        _48f = _49d();
        _4a4();
        this.logger.removeListener(_48d);
        this.logger.addListener(_48d, _48f, _49b);
    };
    var _4a7 = bind(function() {
        _48e = this.logger.getMessages();
        _4a4();
    }, this);
    var _4a8 = bind(function(_4a9) {
        _4a9 = _4a9 || window.event;
        key = _4a9.which || _4a9.keyCode;
        if (key == 13) {
            this.buildAndApplyFilter();
        }
    }, this);
    var _4aa = "display: block; z-index: 1000; left: 0px; bottom: 0px; position: fixed; width: 100%; background-color: white; font: " + this.logFont;
    if (_476) {
        _4aa += "; height: 10em; border-top: 2px solid black";
    } else {
        _4aa += "; height: 100%;";
    }
    _482.style.cssText = _4aa;
    if (!_483) {
        doc.body.appendChild(_482);
    }
    _4aa = { "cssText": "width: 33%; display: inline; font: " + this.logFont };
    _479(_485, { "value": "FATAL|ERROR|WARNING|INFO|DEBUG", "onkeypress": _4a8, "style": _4aa });
    _482.appendChild(_485);
    _479(_486, { "value": ".*", "onkeypress": _4a8, "style": _4aa });
    _482.appendChild(_486);
    _4aa = "width: 8%; display:inline; font: " + this.logFont;
    _487.appendChild(doc.createTextNode("Filter"));
    _487.onclick = bind("buildAndApplyFilter", this);
    _487.style.cssText = _4aa;
    _482.appendChild(_487);
    _488.appendChild(doc.createTextNode("Load"));
    _488.onclick = _4a7;
    _488.style.cssText = _4aa;
    _482.appendChild(_488);
    _489.appendChild(doc.createTextNode("Clear"));
    _489.onclick = _4a2;
    _489.style.cssText = _4aa;
    _482.appendChild(_489);
    _48a.appendChild(doc.createTextNode("Close"));
    _48a.onclick = _4a3;
    _48a.style.cssText = _4aa;
    _482.appendChild(_48a);
    _48b.style.cssText = "overflow: auto; width: 100%";
    _48c.style.cssText = "width: 100%; height: " + (_476 ? "8em" : "100%");
    _48b.appendChild(_48c);
    _482.appendChild(_48b);
    this.buildAndApplyFilter();
    _4a7();
    if (_476) {
        this.win = undefined;
    } else {
        this.win = win;
    }
    this.inline = _476;
    this.closePane = _4a3;
    this.closed = false;
    return this;
};
MochiKit.LoggingPane.LoggingPane.prototype = { "logFont": "8pt Verdana,sans-serif", "colorTable": { "ERROR": "red", "FATAL": "darkred", "WARNING": "blue", "INFO": "black", "DEBUG": "green"} };
MochiKit.LoggingPane.EXPORT_OK = ["LoggingPane"];
MochiKit.LoggingPane.EXPORT = ["createLoggingPane"];
MochiKit.LoggingPane.__new__ = function() {
    this.EXPORT_TAGS = { ":common": this.EXPORT, ":all": MochiKit.Base.concat(this.EXPORT, this.EXPORT_OK) };
    MochiKit.Base.nameFunctions(this);
    MochiKit.LoggingPane._loggingPane = null;
};
MochiKit.LoggingPane.__new__();
MochiKit.Base._exportSymbols(this, MochiKit.LoggingPane);
MochiKit.Base._deps("Color", ["Base", "DOM", "Style"]);
MochiKit.Color.NAME = "MochiKit.Color";
MochiKit.Color.VERSION = "1.4.2";
MochiKit.Color.__repr__ = function() {
    return "[" + this.NAME + " " + this.VERSION + "]";
};
MochiKit.Color.toString = function() {
    return this.__repr__();
};
MochiKit.Color.Color = function(red, _4ac, blue, _4ae) {
    if (typeof (_4ae) == "undefined" || _4ae === null) {
        _4ae = 1;
    }
    this.rgb = { r: red, g: _4ac, b: blue, a: _4ae };
};
MochiKit.Color.Color.prototype = { __class__: MochiKit.Color.Color, colorWithAlpha: function(_4af) {
    var rgb = this.rgb;
    var m = MochiKit.Color;
    return m.Color.fromRGB(rgb.r, rgb.g, rgb.b, _4af);
}, colorWithHue: function(hue) {
    var hsl = this.asHSL();
    hsl.h = hue;
    var m = MochiKit.Color;
    return m.Color.fromHSL(hsl);
}, colorWithSaturation: function(_4b5) {
    var hsl = this.asHSL();
    hsl.s = _4b5;
    var m = MochiKit.Color;
    return m.Color.fromHSL(hsl);
}, colorWithLightness: function(_4b8) {
    var hsl = this.asHSL();
    hsl.l = _4b8;
    var m = MochiKit.Color;
    return m.Color.fromHSL(hsl);
}, darkerColorWithLevel: function(_4bb) {
    var hsl = this.asHSL();
    hsl.l = Math.max(hsl.l - _4bb, 0);
    var m = MochiKit.Color;
    return m.Color.fromHSL(hsl);
}, lighterColorWithLevel: function(_4be) {
    var hsl = this.asHSL();
    hsl.l = Math.min(hsl.l + _4be, 1);
    var m = MochiKit.Color;
    return m.Color.fromHSL(hsl);
}, blendedColor: function(_4c1, _4c2) {
    if (typeof (_4c2) == "undefined" || _4c2 === null) {
        _4c2 = 0.5;
    }
    var sf = 1 - _4c2;
    var s = this.rgb;
    var d = _4c1.rgb;
    var df = _4c2;
    return MochiKit.Color.Color.fromRGB((s.r * sf) + (d.r * df), (s.g * sf) + (d.g * df), (s.b * sf) + (d.b * df), (s.a * sf) + (d.a * df));
}, compareRGB: function(_4c7) {
    var a = this.asRGB();
    var b = _4c7.asRGB();
    return MochiKit.Base.compare([a.r, a.g, a.b, a.a], [b.r, b.g, b.b, b.a]);
}, isLight: function() {
    return this.asHSL().b > 0.5;
}, isDark: function() {
    return (!this.isLight());
}, toHSLString: function() {
    var c = this.asHSL();
    var ccc = MochiKit.Color.clampColorComponent;
    var rval = this._hslString;
    if (!rval) {
        var mid = (ccc(c.h, 360).toFixed(0) + "," + ccc(c.s, 100).toPrecision(4) + "%" + "," + ccc(c.l, 100).toPrecision(4) + "%");
        var a = c.a;
        if (a >= 1) {
            a = 1;
            rval = "hsl(" + mid + ")";
        } else {
            if (a <= 0) {
                a = 0;
            }
            rval = "hsla(" + mid + "," + a + ")";
        }
        this._hslString = rval;
    }
    return rval;
}, toRGBString: function() {
    var c = this.rgb;
    var ccc = MochiKit.Color.clampColorComponent;
    var rval = this._rgbString;
    if (!rval) {
        var mid = (ccc(c.r, 255).toFixed(0) + "," + ccc(c.g, 255).toFixed(0) + "," + ccc(c.b, 255).toFixed(0));
        if (c.a != 1) {
            rval = "rgba(" + mid + "," + c.a + ")";
        } else {
            rval = "rgb(" + mid + ")";
        }
        this._rgbString = rval;
    }
    return rval;
}, asRGB: function() {
    return MochiKit.Base.clone(this.rgb);
}, toHexString: function() {
    var m = MochiKit.Color;
    var c = this.rgb;
    var ccc = MochiKit.Color.clampColorComponent;
    var rval = this._hexString;
    if (!rval) {
        rval = ("#" + m.toColorPart(ccc(c.r, 255)) + m.toColorPart(ccc(c.g, 255)) + m.toColorPart(ccc(c.b, 255)));
        this._hexString = rval;
    }
    return rval;
}, asHSV: function() {
    var hsv = this.hsv;
    var c = this.rgb;
    if (typeof (hsv) == "undefined" || hsv === null) {
        hsv = MochiKit.Color.rgbToHSV(this.rgb);
        this.hsv = hsv;
    }
    return MochiKit.Base.clone(hsv);
}, asHSL: function() {
    var hsl = this.hsl;
    var c = this.rgb;
    if (typeof (hsl) == "undefined" || hsl === null) {
        hsl = MochiKit.Color.rgbToHSL(this.rgb);
        this.hsl = hsl;
    }
    return MochiKit.Base.clone(hsl);
}, toString: function() {
    return this.toRGBString();
}, repr: function() {
    var c = this.rgb;
    var col = [c.r, c.g, c.b, c.a];
    return this.__class__.NAME + "(" + col.join(", ") + ")";
} 
};
MochiKit.Base.update(MochiKit.Color.Color, { fromRGB: function(red, _4de, blue, _4e0) {
    var _4e1 = MochiKit.Color.Color;
    if (arguments.length == 1) {
        var rgb = red;
        red = rgb.r;
        _4de = rgb.g;
        blue = rgb.b;
        if (typeof (rgb.a) == "undefined") {
            _4e0 = undefined;
        } else {
            _4e0 = rgb.a;
        }
    }
    return new _4e1(red, _4de, blue, _4e0);
}, fromHSL: function(hue, _4e4, _4e5, _4e6) {
    var m = MochiKit.Color;
    return m.Color.fromRGB(m.hslToRGB.apply(m, arguments));
}, fromHSV: function(hue, _4e9, _4ea, _4eb) {
    var m = MochiKit.Color;
    return m.Color.fromRGB(m.hsvToRGB.apply(m, arguments));
}, fromName: function(name) {
    var _4ee = MochiKit.Color.Color;
    if (name.charAt(0) == "\"") {
        name = name.substr(1, name.length - 2);
    }
    var _4ef = _4ee._namedColors[name.toLowerCase()];
    if (typeof (_4ef) == "string") {
        return _4ee.fromHexString(_4ef);
    } else {
        if (name == "transparent") {
            return _4ee.transparentColor();
        }
    }
    return null;
}, fromString: function(_4f0) {
    var self = MochiKit.Color.Color;
    var _4f2 = _4f0.substr(0, 3);
    if (_4f2 == "rgb") {
        return self.fromRGBString(_4f0);
    } else {
        if (_4f2 == "hsl") {
            return self.fromHSLString(_4f0);
        } else {
            if (_4f0.charAt(0) == "#") {
                return self.fromHexString(_4f0);
            }
        }
    }
    return self.fromName(_4f0);
}, fromHexString: function(_4f3) {
    if (_4f3.charAt(0) == "#") {
        _4f3 = _4f3.substring(1);
    }
    var _4f4 = [];
    var i, hex;
    if (_4f3.length == 3) {
        for (i = 0; i < 3; i++) {
            hex = _4f3.substr(i, 1);
            _4f4.push(parseInt(hex + hex, 16) / 255);
        }
    } else {
        for (i = 0; i < 6; i += 2) {
            hex = _4f3.substr(i, 2);
            _4f4.push(parseInt(hex, 16) / 255);
        }
    }
    var _4f7 = MochiKit.Color.Color;
    return _4f7.fromRGB.apply(_4f7, _4f4);
}, _fromColorString: function(pre, _4f9, _4fa, _4fb) {
    if (_4fb.indexOf(pre) === 0) {
        _4fb = _4fb.substring(_4fb.indexOf("(", 3) + 1, _4fb.length - 1);
    }
    var _4fc = _4fb.split(/\s*,\s*/);
    var _4fd = [];
    for (var i = 0; i < _4fc.length; i++) {
        var c = _4fc[i];
        var val;
        var _501 = c.substring(c.length - 3);
        if (c.charAt(c.length - 1) == "%") {
            val = 0.01 * parseFloat(c.substring(0, c.length - 1));
        } else {
            if (_501 == "deg") {
                val = parseFloat(c) / 360;
            } else {
                if (_501 == "rad") {
                    val = parseFloat(c) / (Math.PI * 2);
                } else {
                    val = _4fa[i] * parseFloat(c);
                }
            }
        }
        _4fd.push(val);
    }
    return this[_4f9].apply(this, _4fd);
}, fromComputedStyle: function(elem, _503) {
    var d = MochiKit.DOM;
    var cls = MochiKit.Color.Color;
    for (elem = d.getElement(elem); elem; elem = elem.parentNode) {
        var _506 = MochiKit.Style.getStyle.apply(d, arguments);
        if (!_506) {
            continue;
        }
        var _507 = cls.fromString(_506);
        if (!_507) {
            break;
        }
        if (_507.asRGB().a > 0) {
            return _507;
        }
    }
    return null;
}, fromBackground: function(elem) {
    var cls = MochiKit.Color.Color;
    return cls.fromComputedStyle(elem, "backgroundColor", "background-color") || cls.whiteColor();
}, fromText: function(elem) {
    var cls = MochiKit.Color.Color;
    return cls.fromComputedStyle(elem, "color", "color") || cls.blackColor();
}, namedColors: function() {
    return MochiKit.Base.clone(MochiKit.Color.Color._namedColors);
} 
});
MochiKit.Base.update(MochiKit.Color, { clampColorComponent: function(v, _50d) {
    v *= _50d;
    if (v < 0) {
        return 0;
    } else {
        if (v > _50d) {
            return _50d;
        } else {
            return v;
        }
    }
}, _hslValue: function(n1, n2, hue) {
    if (hue > 6) {
        hue -= 6;
    } else {
        if (hue < 0) {
            hue += 6;
        }
    }
    var val;
    if (hue < 1) {
        val = n1 + (n2 - n1) * hue;
    } else {
        if (hue < 3) {
            val = n2;
        } else {
            if (hue < 4) {
                val = n1 + (n2 - n1) * (4 - hue);
            } else {
                val = n1;
            }
        }
    }
    return val;
}, hsvToRGB: function(hue, _513, _514, _515) {
    if (arguments.length == 1) {
        var hsv = hue;
        hue = hsv.h;
        _513 = hsv.s;
        _514 = hsv.v;
        _515 = hsv.a;
    }
    var red;
    var _518;
    var blue;
    if (_513 === 0) {
        red = _514;
        _518 = _514;
        blue = _514;
    } else {
        var i = Math.floor(hue * 6);
        var f = (hue * 6) - i;
        var p = _514 * (1 - _513);
        var q = _514 * (1 - (_513 * f));
        var t = _514 * (1 - (_513 * (1 - f)));
        switch (i) {
            case 1:
                red = q;
                _518 = _514;
                blue = p;
                break;
            case 2:
                red = p;
                _518 = _514;
                blue = t;
                break;
            case 3:
                red = p;
                _518 = q;
                blue = _514;
                break;
            case 4:
                red = t;
                _518 = p;
                blue = _514;
                break;
            case 5:
                red = _514;
                _518 = p;
                blue = q;
                break;
            case 6:
            case 0:
                red = _514;
                _518 = t;
                blue = p;
                break;
        }
    }
    return { r: red, g: _518, b: blue, a: _515 };
}, hslToRGB: function(hue, _520, _521, _522) {
    if (arguments.length == 1) {
        var hsl = hue;
        hue = hsl.h;
        _520 = hsl.s;
        _521 = hsl.l;
        _522 = hsl.a;
    }
    var red;
    var _525;
    var blue;
    if (_520 === 0) {
        red = _521;
        _525 = _521;
        blue = _521;
    } else {
        var m2;
        if (_521 <= 0.5) {
            m2 = _521 * (1 + _520);
        } else {
            m2 = _521 + _520 - (_521 * _520);
        }
        var m1 = (2 * _521) - m2;
        var f = MochiKit.Color._hslValue;
        var h6 = hue * 6;
        red = f(m1, m2, h6 + 2);
        _525 = f(m1, m2, h6);
        blue = f(m1, m2, h6 - 2);
    }
    return { r: red, g: _525, b: blue, a: _522 };
}, rgbToHSV: function(red, _52c, blue, _52e) {
    if (arguments.length == 1) {
        var rgb = red;
        red = rgb.r;
        _52c = rgb.g;
        blue = rgb.b;
        _52e = rgb.a;
    }
    var max = Math.max(Math.max(red, _52c), blue);
    var min = Math.min(Math.min(red, _52c), blue);
    var hue;
    var _533;
    var _534 = max;
    if (min == max) {
        hue = 0;
        _533 = 0;
    } else {
        var _535 = (max - min);
        _533 = _535 / max;
        if (red == max) {
            hue = (_52c - blue) / _535;
        } else {
            if (_52c == max) {
                hue = 2 + ((blue - red) / _535);
            } else {
                hue = 4 + ((red - _52c) / _535);
            }
        }
        hue /= 6;
        if (hue < 0) {
            hue += 1;
        }
        if (hue > 1) {
            hue -= 1;
        }
    }
    return { h: hue, s: _533, v: _534, a: _52e };
}, rgbToHSL: function(red, _537, blue, _539) {
    if (arguments.length == 1) {
        var rgb = red;
        red = rgb.r;
        _537 = rgb.g;
        blue = rgb.b;
        _539 = rgb.a;
    }
    var max = Math.max(red, Math.max(_537, blue));
    var min = Math.min(red, Math.min(_537, blue));
    var hue;
    var _53e;
    var _53f = (max + min) / 2;
    var _540 = max - min;
    if (_540 === 0) {
        hue = 0;
        _53e = 0;
    } else {
        if (_53f <= 0.5) {
            _53e = _540 / (max + min);
        } else {
            _53e = _540 / (2 - max - min);
        }
        if (red == max) {
            hue = (_537 - blue) / _540;
        } else {
            if (_537 == max) {
                hue = 2 + ((blue - red) / _540);
            } else {
                hue = 4 + ((red - _537) / _540);
            }
        }
        hue /= 6;
        if (hue < 0) {
            hue += 1;
        }
        if (hue > 1) {
            hue -= 1;
        }
    }
    return { h: hue, s: _53e, l: _53f, a: _539 };
}, toColorPart: function(num) {
    num = Math.round(num);
    var _542 = num.toString(16);
    if (num < 16) {
        return "0" + _542;
    }
    return _542;
}, __new__: function() {
    var m = MochiKit.Base;
    this.Color.fromRGBString = m.bind(this.Color._fromColorString, this.Color, "rgb", "fromRGB", [1 / 255, 1 / 255, 1 / 255, 1]);
    this.Color.fromHSLString = m.bind(this.Color._fromColorString, this.Color, "hsl", "fromHSL", [1 / 360, 0.01, 0.01, 1]);
    var _544 = 1 / 3;
    var _545 = { black: [0, 0, 0], blue: [0, 0, 1], brown: [0.6, 0.4, 0.2], cyan: [0, 1, 1], darkGray: [_544, _544, _544], gray: [0.5, 0.5, 0.5], green: [0, 1, 0], lightGray: [2 * _544, 2 * _544, 2 * _544], magenta: [1, 0, 1], orange: [1, 0.5, 0], purple: [0.5, 0, 0.5], red: [1, 0, 0], transparent: [0, 0, 0, 0], white: [1, 1, 1], yellow: [1, 1, 0] };
    var _546 = function(name, r, g, b, a) {
        var rval = this.fromRGB(r, g, b, a);
        this[name] = function() {
            return rval;
        };
        return rval;
    };
    for (var k in _545) {
        var name = k + "Color";
        var _54f = m.concat([_546, this.Color, name], _545[k]);
        this.Color[name] = m.bind.apply(null, _54f);
    }
    var _550 = function() {
        for (var i = 0; i < arguments.length; i++) {
            if (!(arguments[i] instanceof MochiKit.Color.Color)) {
                return false;
            }
        }
        return true;
    };
    var _552 = function(a, b) {
        return a.compareRGB(b);
    };
    m.nameFunctions(this);
    m.registerComparator(this.Color.NAME, _550, _552);
    this.EXPORT_TAGS = { ":common": this.EXPORT, ":all": m.concat(this.EXPORT, this.EXPORT_OK) };
} 
});
MochiKit.Color.EXPORT = ["Color"];
MochiKit.Color.EXPORT_OK = ["clampColorComponent", "rgbToHSL", "hslToRGB", "rgbToHSV", "hsvToRGB", "toColorPart"];
MochiKit.Color.__new__();
MochiKit.Base._exportSymbols(this, MochiKit.Color);
MochiKit.Color.Color._namedColors = { aliceblue: "#f0f8ff", antiquewhite: "#faebd7", aqua: "#00ffff", aquamarine: "#7fffd4", azure: "#f0ffff", beige: "#f5f5dc", bisque: "#ffe4c4", black: "#000000", blanchedalmond: "#ffebcd", blue: "#0000ff", blueviolet: "#8a2be2", brown: "#a52a2a", burlywood: "#deb887", cadetblue: "#5f9ea0", chartreuse: "#7fff00", chocolate: "#d2691e", coral: "#ff7f50", cornflowerblue: "#6495ed", cornsilk: "#fff8dc", crimson: "#dc143c", cyan: "#00ffff", darkblue: "#00008b", darkcyan: "#008b8b", darkgoldenrod: "#b8860b", darkgray: "#a9a9a9", darkgreen: "#006400", darkgrey: "#a9a9a9", darkkhaki: "#bdb76b", darkmagenta: "#8b008b", darkolivegreen: "#556b2f", darkorange: "#ff8c00", darkorchid: "#9932cc", darkred: "#8b0000", darksalmon: "#e9967a", darkseagreen: "#8fbc8f", darkslateblue: "#483d8b", darkslategray: "#2f4f4f", darkslategrey: "#2f4f4f", darkturquoise: "#00ced1", darkviolet: "#9400d3", deeppink: "#ff1493", deepskyblue: "#00bfff", dimgray: "#696969", dimgrey: "#696969", dodgerblue: "#1e90ff", firebrick: "#b22222", floralwhite: "#fffaf0", forestgreen: "#228b22", fuchsia: "#ff00ff", gainsboro: "#dcdcdc", ghostwhite: "#f8f8ff", gold: "#ffd700", goldenrod: "#daa520", gray: "#808080", green: "#008000", greenyellow: "#adff2f", grey: "#808080", honeydew: "#f0fff0", hotpink: "#ff69b4", indianred: "#cd5c5c", indigo: "#4b0082", ivory: "#fffff0", khaki: "#f0e68c", lavender: "#e6e6fa", lavenderblush: "#fff0f5", lawngreen: "#7cfc00", lemonchiffon: "#fffacd", lightblue: "#add8e6", lightcoral: "#f08080", lightcyan: "#e0ffff", lightgoldenrodyellow: "#fafad2", lightgray: "#d3d3d3", lightgreen: "#90ee90", lightgrey: "#d3d3d3", lightpink: "#ffb6c1", lightsalmon: "#ffa07a", lightseagreen: "#20b2aa", lightskyblue: "#87cefa", lightslategray: "#778899", lightslategrey: "#778899", lightsteelblue: "#b0c4de", lightyellow: "#ffffe0", lime: "#00ff00", limegreen: "#32cd32", linen: "#faf0e6", magenta: "#ff00ff", maroon: "#800000", mediumaquamarine: "#66cdaa", mediumblue: "#0000cd", mediumorchid: "#ba55d3", mediumpurple: "#9370db", mediumseagreen: "#3cb371", mediumslateblue: "#7b68ee", mediumspringgreen: "#00fa9a", mediumturquoise: "#48d1cc", mediumvioletred: "#c71585", midnightblue: "#191970", mintcream: "#f5fffa", mistyrose: "#ffe4e1", moccasin: "#ffe4b5", navajowhite: "#ffdead", navy: "#000080", oldlace: "#fdf5e6", olive: "#808000", olivedrab: "#6b8e23", orange: "#ffa500", orangered: "#ff4500", orchid: "#da70d6", palegoldenrod: "#eee8aa", palegreen: "#98fb98", paleturquoise: "#afeeee", palevioletred: "#db7093", papayawhip: "#ffefd5", peachpuff: "#ffdab9", peru: "#cd853f", pink: "#ffc0cb", plum: "#dda0dd", powderblue: "#b0e0e6", purple: "#800080", red: "#ff0000", rosybrown: "#bc8f8f", royalblue: "#4169e1", saddlebrown: "#8b4513", salmon: "#fa8072", sandybrown: "#f4a460", seagreen: "#2e8b57", seashell: "#fff5ee", sienna: "#a0522d", silver: "#c0c0c0", skyblue: "#87ceeb", slateblue: "#6a5acd", slategray: "#708090", slategrey: "#708090", snow: "#fffafa", springgreen: "#00ff7f", steelblue: "#4682b4", tan: "#d2b48c", teal: "#008080", thistle: "#d8bfd8", tomato: "#ff6347", turquoise: "#40e0d0", violet: "#ee82ee", wheat: "#f5deb3", white: "#ffffff", whitesmoke: "#f5f5f5", yellow: "#ffff00", yellowgreen: "#9acd32" };
MochiKit.Base._deps("Signal", ["Base", "DOM", "Style"]);
MochiKit.Signal.NAME = "MochiKit.Signal";
MochiKit.Signal.VERSION = "1.4.2";
MochiKit.Signal._observers = [];
MochiKit.Signal.Event = function(src, e) {
    this._event = e || window.event;
    this._src = src;
};
MochiKit.Base.update(MochiKit.Signal.Event.prototype, { __repr__: function() {
    var repr = MochiKit.Base.repr;
    var str = "{event(): " + repr(this.event()) + ", src(): " + repr(this.src()) + ", type(): " + repr(this.type()) + ", target(): " + repr(this.target());
    if (this.type() && this.type().indexOf("key") === 0 || this.type().indexOf("mouse") === 0 || this.type().indexOf("click") != -1 || this.type() == "contextmenu") {
        str += ", modifier(): " + "{alt: " + repr(this.modifier().alt) + ", ctrl: " + repr(this.modifier().ctrl) + ", meta: " + repr(this.modifier().meta) + ", shift: " + repr(this.modifier().shift) + ", any: " + repr(this.modifier().any) + "}";
    }
    if (this.type() && this.type().indexOf("key") === 0) {
        str += ", key(): {code: " + repr(this.key().code) + ", string: " + repr(this.key().string) + "}";
    }
    if (this.type() && (this.type().indexOf("mouse") === 0 || this.type().indexOf("click") != -1 || this.type() == "contextmenu")) {
        str += ", mouse(): {page: " + repr(this.mouse().page) + ", client: " + repr(this.mouse().client);
        if (this.type() != "mousemove" && this.type() != "mousewheel") {
            str += ", button: {left: " + repr(this.mouse().button.left) + ", middle: " + repr(this.mouse().button.middle) + ", right: " + repr(this.mouse().button.right) + "}";
        }
        if (this.type() == "mousewheel") {
            str += ", wheel: " + repr(this.mouse().wheel);
        }
        str += "}";
    }
    if (this.type() == "mouseover" || this.type() == "mouseout" || this.type() == "mouseenter" || this.type() == "mouseleave") {
        str += ", relatedTarget(): " + repr(this.relatedTarget());
    }
    str += "}";
    return str;
}, toString: function() {
    return this.__repr__();
}, src: function() {
    return this._src;
}, event: function() {
    return this._event;
}, type: function() {
    if (this._event.type === "DOMMouseScroll") {
        return "mousewheel";
    } else {
        return this._event.type || undefined;
    }
}, target: function() {
    return this._event.target || this._event.srcElement;
}, _relatedTarget: null, relatedTarget: function() {
    if (this._relatedTarget !== null) {
        return this._relatedTarget;
    }
    var elem = null;
    if (this.type() == "mouseover" || this.type() == "mouseenter") {
        elem = (this._event.relatedTarget || this._event.fromElement);
    } else {
        if (this.type() == "mouseout" || this.type() == "mouseleave") {
            elem = (this._event.relatedTarget || this._event.toElement);
        }
    }
    try {
        if (elem !== null && elem.nodeType !== null) {
            this._relatedTarget = elem;
            return elem;
        }
    }
    catch (ignore) {
    }
    return undefined;
}, _modifier: null, modifier: function() {
    if (this._modifier !== null) {
        return this._modifier;
    }
    var m = {};
    m.alt = this._event.altKey;
    m.ctrl = this._event.ctrlKey;
    m.meta = this._event.metaKey || false;
    m.shift = this._event.shiftKey;
    m.any = m.alt || m.ctrl || m.shift || m.meta;
    this._modifier = m;
    return m;
}, _key: null, key: function() {
    if (this._key !== null) {
        return this._key;
    }
    var k = {};
    if (this.type() && this.type().indexOf("key") === 0) {
        if (this.type() == "keydown" || this.type() == "keyup") {
            k.code = this._event.keyCode;
            k.string = (MochiKit.Signal._specialKeys[k.code] || "KEY_UNKNOWN");
            this._key = k;
            return k;
        } else {
            if (this.type() == "keypress") {
                k.code = 0;
                k.string = "";
                if (typeof (this._event.charCode) != "undefined" && this._event.charCode !== 0 && !MochiKit.Signal._specialMacKeys[this._event.charCode]) {
                    k.code = this._event.charCode;
                    k.string = String.fromCharCode(k.code);
                } else {
                    if (this._event.keyCode && typeof (this._event.charCode) == "undefined") {
                        k.code = this._event.keyCode;
                        k.string = String.fromCharCode(k.code);
                    }
                }
                this._key = k;
                return k;
            }
        }
    }
    return undefined;
}, _mouse: null, mouse: function() {
    if (this._mouse !== null) {
        return this._mouse;
    }
    var m = {};
    var e = this._event;
    if (this.type() && (this.type().indexOf("mouse") === 0 || this.type().indexOf("click") != -1 || this.type() == "contextmenu")) {
        m.client = new MochiKit.Style.Coordinates(0, 0);
        if (e.clientX || e.clientY) {
            m.client.x = (!e.clientX || e.clientX < 0) ? 0 : e.clientX;
            m.client.y = (!e.clientY || e.clientY < 0) ? 0 : e.clientY;
        }
        m.page = new MochiKit.Style.Coordinates(0, 0);
        if (e.pageX || e.pageY) {
            m.page.x = (!e.pageX || e.pageX < 0) ? 0 : e.pageX;
            m.page.y = (!e.pageY || e.pageY < 0) ? 0 : e.pageY;
        } else {
            var de = MochiKit.DOM._document.documentElement;
            var b = MochiKit.DOM._document.body;
            m.page.x = e.clientX + (de.scrollLeft || b.scrollLeft) - (de.clientLeft || 0);
            m.page.y = e.clientY + (de.scrollTop || b.scrollTop) - (de.clientTop || 0);
        }
        if (this.type() != "mousemove" && this.type() != "mousewheel") {
            m.button = {};
            m.button.left = false;
            m.button.right = false;
            m.button.middle = false;
            if (e.which) {
                m.button.left = (e.which == 1);
                m.button.middle = (e.which == 2);
                m.button.right = (e.which == 3);
            } else {
                m.button.left = !!(e.button & 1);
                m.button.right = !!(e.button & 2);
                m.button.middle = !!(e.button & 4);
            }
        }
        if (this.type() == "mousewheel") {
            m.wheel = new MochiKit.Style.Coordinates(0, 0);
            if (e.wheelDeltaX || e.wheelDeltaY) {
                m.wheel.x = e.wheelDeltaX / -40 || 0;
                m.wheel.y = e.wheelDeltaY / -40 || 0;
            } else {
                if (e.wheelDelta) {
                    m.wheel.y = e.wheelDelta / -40;
                } else {
                    m.wheel.y = e.detail || 0;
                }
            }
        }
        this._mouse = m;
        return m;
    }
    return undefined;
}, stop: function() {
    this.stopPropagation();
    this.preventDefault();
}, stopPropagation: function() {
    if (this._event.stopPropagation) {
        this._event.stopPropagation();
    } else {
        this._event.cancelBubble = true;
    }
}, preventDefault: function() {
    if (this._event.preventDefault) {
        this._event.preventDefault();
    } else {
        if (this._confirmUnload === null) {
            this._event.returnValue = false;
        }
    }
}, _confirmUnload: null, confirmUnload: function(msg) {
    if (this.type() == "beforeunload") {
        this._confirmUnload = msg;
        this._event.returnValue = msg;
    }
} 
});
MochiKit.Signal._specialMacKeys = { 3: "KEY_ENTER", 63289: "KEY_NUM_PAD_CLEAR", 63276: "KEY_PAGE_UP", 63277: "KEY_PAGE_DOWN", 63275: "KEY_END", 63273: "KEY_HOME", 63234: "KEY_ARROW_LEFT", 63232: "KEY_ARROW_UP", 63235: "KEY_ARROW_RIGHT", 63233: "KEY_ARROW_DOWN", 63302: "KEY_INSERT", 63272: "KEY_DELETE" };
(function() {
    var _561 = MochiKit.Signal._specialMacKeys;
    for (i = 63236; i <= 63242; i++) {
        _561[i] = "KEY_F" + (i - 63236 + 1);
    }
})();
MochiKit.Signal._specialKeys = { 8: "KEY_BACKSPACE", 9: "KEY_TAB", 12: "KEY_NUM_PAD_CLEAR", 13: "KEY_ENTER", 16: "KEY_SHIFT", 17: "KEY_CTRL", 18: "KEY_ALT", 19: "KEY_PAUSE", 20: "KEY_CAPS_LOCK", 27: "KEY_ESCAPE", 32: "KEY_SPACEBAR", 33: "KEY_PAGE_UP", 34: "KEY_PAGE_DOWN", 35: "KEY_END", 36: "KEY_HOME", 37: "KEY_ARROW_LEFT", 38: "KEY_ARROW_UP", 39: "KEY_ARROW_RIGHT", 40: "KEY_ARROW_DOWN", 44: "KEY_PRINT_SCREEN", 45: "KEY_INSERT", 46: "KEY_DELETE", 59: "KEY_SEMICOLON", 91: "KEY_WINDOWS_LEFT", 92: "KEY_WINDOWS_RIGHT", 93: "KEY_SELECT", 106: "KEY_NUM_PAD_ASTERISK", 107: "KEY_NUM_PAD_PLUS_SIGN", 109: "KEY_NUM_PAD_HYPHEN-MINUS", 110: "KEY_NUM_PAD_FULL_STOP", 111: "KEY_NUM_PAD_SOLIDUS", 144: "KEY_NUM_LOCK", 145: "KEY_SCROLL_LOCK", 186: "KEY_SEMICOLON", 187: "KEY_EQUALS_SIGN", 188: "KEY_COMMA", 189: "KEY_HYPHEN-MINUS", 190: "KEY_FULL_STOP", 191: "KEY_SOLIDUS", 192: "KEY_GRAVE_ACCENT", 219: "KEY_LEFT_SQUARE_BRACKET", 220: "KEY_REVERSE_SOLIDUS", 221: "KEY_RIGHT_SQUARE_BRACKET", 222: "KEY_APOSTROPHE" };
(function() {
    var _562 = MochiKit.Signal._specialKeys;
    for (var i = 48; i <= 57; i++) {
        _562[i] = "KEY_" + (i - 48);
    }
    for (i = 65; i <= 90; i++) {
        _562[i] = "KEY_" + String.fromCharCode(i);
    }
    for (i = 96; i <= 105; i++) {
        _562[i] = "KEY_NUM_PAD_" + (i - 96);
    }
    for (i = 112; i <= 123; i++) {
        _562[i] = "KEY_F" + (i - 112 + 1);
    }
})();
MochiKit.Signal.Ident = function(_564) {
    this.source = _564.source;
    this.signal = _564.signal;
    this.listener = _564.listener;
    this.isDOM = _564.isDOM;
    this.objOrFunc = _564.objOrFunc;
    this.funcOrStr = _564.funcOrStr;
    this.connected = _564.connected;
};
MochiKit.Signal.Ident.prototype = {};
MochiKit.Base.update(MochiKit.Signal, { __repr__: function() {
    return "[" + this.NAME + " " + this.VERSION + "]";
}, toString: function() {
    return this.__repr__();
}, _unloadCache: function() {
    var self = MochiKit.Signal;
    var _566 = self._observers;
    for (var i = 0; i < _566.length; i++) {
        if (_566[i].signal !== "onload" && _566[i].signal !== "onunload") {
            self._disconnect(_566[i]);
        }
    }
}, _listener: function(src, sig, func, obj, _56c) {
    var self = MochiKit.Signal;
    var E = self.Event;
    if (!_56c) {
        if (typeof (func.im_self) == "undefined") {
            return MochiKit.Base.bindLate(func, obj);
        } else {
            return func;
        }
    }
    obj = obj || src;
    if (typeof (func) == "string") {
        if (sig === "onload" || sig === "onunload") {
            return function(_56f) {
                obj[func].apply(obj, [new E(src, _56f)]);
                var _570 = new MochiKit.Signal.Ident({ source: src, signal: sig, objOrFunc: obj, funcOrStr: func });
                MochiKit.Signal._disconnect(_570);
            };
        } else {
            return function(_571) {
                obj[func].apply(obj, [new E(src, _571)]);
            };
        }
    } else {
        if (sig === "onload" || sig === "onunload") {
            return function(_572) {
                func.apply(obj, [new E(src, _572)]);
                var _573 = new MochiKit.Signal.Ident({ source: src, signal: sig, objOrFunc: func });
                MochiKit.Signal._disconnect(_573);
            };
        } else {
            return function(_574) {
                func.apply(obj, [new E(src, _574)]);
            };
        }
    }
}, _browserAlreadyHasMouseEnterAndLeave: function() {
    return /MSIE/.test(navigator.userAgent);
}, _browserLacksMouseWheelEvent: function() {
    return /Gecko\//.test(navigator.userAgent);
}, _mouseEnterListener: function(src, sig, func, obj) {
    var E = MochiKit.Signal.Event;
    return function(_57a) {
        var e = new E(src, _57a);
        try {
            e.relatedTarget().nodeName;
        }
        catch (err) {
            return;
        }
        e.stop();
        if (MochiKit.DOM.isChildNode(e.relatedTarget(), src)) {
            return;
        }
        e.type = function() {
            return sig;
        };
        if (typeof (func) == "string") {
            return obj[func].apply(obj, [e]);
        } else {
            return func.apply(obj, [e]);
        }
    };
}, _getDestPair: function(_57c, _57d) {
    var obj = null;
    var func = null;
    if (typeof (_57d) != "undefined") {
        obj = _57c;
        func = _57d;
        if (typeof (_57d) == "string") {
            if (typeof (_57c[_57d]) != "function") {
                throw new Error("'funcOrStr' must be a function on 'objOrFunc'");
            }
        } else {
            if (typeof (_57d) != "function") {
                throw new Error("'funcOrStr' must be a function or string");
            }
        }
    } else {
        if (typeof (_57c) != "function") {
            throw new Error("'objOrFunc' must be a function if 'funcOrStr' is not given");
        } else {
            func = _57c;
        }
    }
    return [obj, func];
}, connect: function(src, sig, _582, _583) {
    src = MochiKit.DOM.getElement(src);
    var self = MochiKit.Signal;
    if (typeof (sig) != "string") {
        throw new Error("'sig' must be a string");
    }
    var _585 = self._getDestPair(_582, _583);
    var obj = _585[0];
    var func = _585[1];
    if (typeof (obj) == "undefined" || obj === null) {
        obj = src;
    }
    var _588 = !!(src.addEventListener || src.attachEvent);
    if (_588 && (sig === "onmouseenter" || sig === "onmouseleave") && !self._browserAlreadyHasMouseEnterAndLeave()) {
        var _589 = self._mouseEnterListener(src, sig.substr(2), func, obj);
        if (sig === "onmouseenter") {
            sig = "onmouseover";
        } else {
            sig = "onmouseout";
        }
    } else {
        if (_588 && sig == "onmousewheel" && self._browserLacksMouseWheelEvent()) {
            var _589 = self._listener(src, sig, func, obj, _588);
            sig = "onDOMMouseScroll";
        } else {
            var _589 = self._listener(src, sig, func, obj, _588);
        }
    }
    if (src.addEventListener) {
        src.addEventListener(sig.substr(2), _589, false);
    } else {
        if (src.attachEvent) {
            src.attachEvent(sig, _589);
        }
    }
    var _58a = new MochiKit.Signal.Ident({ source: src, signal: sig, listener: _589, isDOM: _588, objOrFunc: _582, funcOrStr: _583, connected: true });
    self._observers.push(_58a);
    if (!_588 && typeof (src.__connect__) == "function") {
        var args = MochiKit.Base.extend([_58a], arguments, 1);
        src.__connect__.apply(src, args);
    }
    return _58a;
}, _disconnect: function(_58c) {
    if (!_58c.connected) {
        return;
    }
    _58c.connected = false;
    var src = _58c.source;
    var sig = _58c.signal;
    var _58f = _58c.listener;
    if (!_58c.isDOM) {
        if (typeof (src.__disconnect__) == "function") {
            src.__disconnect__(_58c, sig, _58c.objOrFunc, _58c.funcOrStr);
        }
        return;
    }
    if (src.removeEventListener) {
        src.removeEventListener(sig.substr(2), _58f, false);
    } else {
        if (src.detachEvent) {
            src.detachEvent(sig, _58f);
        } else {
            throw new Error("'src' must be a DOM element");
        }
    }
}, disconnect: function(_590) {
    var self = MochiKit.Signal;
    var _592 = self._observers;
    var m = MochiKit.Base;
    if (arguments.length > 1) {
        var src = MochiKit.DOM.getElement(arguments[0]);
        var sig = arguments[1];
        var obj = arguments[2];
        var func = arguments[3];
        for (var i = _592.length - 1; i >= 0; i--) {
            var o = _592[i];
            if (o.source === src && o.signal === sig && o.objOrFunc === obj && o.funcOrStr === func) {
                self._disconnect(o);
                if (!self._lock) {
                    _592.splice(i, 1);
                } else {
                    self._dirty = true;
                }
                return true;
            }
        }
    } else {
        var idx = m.findIdentical(_592, _590);
        if (idx >= 0) {
            self._disconnect(_590);
            if (!self._lock) {
                _592.splice(idx, 1);
            } else {
                self._dirty = true;
            }
            return true;
        }
    }
    return false;
}, disconnectAllTo: function(_59b, _59c) {
    var self = MochiKit.Signal;
    var _59e = self._observers;
    var _59f = self._disconnect;
    var _5a0 = self._lock;
    var _5a1 = self._dirty;
    if (typeof (_59c) === "undefined") {
        _59c = null;
    }
    for (var i = _59e.length - 1; i >= 0; i--) {
        var _5a3 = _59e[i];
        if (_5a3.objOrFunc === _59b && (_59c === null || _5a3.funcOrStr === _59c)) {
            _59f(_5a3);
            if (_5a0) {
                _5a1 = true;
            } else {
                _59e.splice(i, 1);
            }
        }
    }
    self._dirty = _5a1;
}, disconnectAll: function(src, sig) {
    src = MochiKit.DOM.getElement(src);
    var m = MochiKit.Base;
    var _5a7 = m.flattenArguments(m.extend(null, arguments, 1));
    var self = MochiKit.Signal;
    var _5a9 = self._disconnect;
    var _5aa = self._observers;
    var i, _5ac;
    var _5ad = self._lock;
    var _5ae = self._dirty;
    if (_5a7.length === 0) {
        for (i = _5aa.length - 1; i >= 0; i--) {
            _5ac = _5aa[i];
            if (_5ac.source === src) {
                _5a9(_5ac);
                if (!_5ad) {
                    _5aa.splice(i, 1);
                } else {
                    _5ae = true;
                }
            }
        }
    } else {
        var sigs = {};
        for (i = 0; i < _5a7.length; i++) {
            sigs[_5a7[i]] = true;
        }
        for (i = _5aa.length - 1; i >= 0; i--) {
            _5ac = _5aa[i];
            if (_5ac.source === src && _5ac.signal in sigs) {
                _5a9(_5ac);
                if (!_5ad) {
                    _5aa.splice(i, 1);
                } else {
                    _5ae = true;
                }
            }
        }
    }
    self._dirty = _5ae;
}, signal: function(src, sig) {
    var self = MochiKit.Signal;
    var _5b3 = self._observers;
    src = MochiKit.DOM.getElement(src);
    var args = MochiKit.Base.extend(null, arguments, 2);
    var _5b5 = [];
    self._lock = true;
    for (var i = 0; i < _5b3.length; i++) {
        var _5b7 = _5b3[i];
        if (_5b7.source === src && _5b7.signal === sig && _5b7.connected) {
            try {
                _5b7.listener.apply(src, args);
            }
            catch (e) {
                _5b5.push(e);
            }
        }
    }
    self._lock = false;
    if (self._dirty) {
        self._dirty = false;
        for (var i = _5b3.length - 1; i >= 0; i--) {
            if (!_5b3[i].connected) {
                _5b3.splice(i, 1);
            }
        }
    }
    if (_5b5.length == 1) {
        throw _5b5[0];
    } else {
        if (_5b5.length > 1) {
            var e = new Error("Multiple errors thrown in handling 'sig', see errors property");
            e.errors = _5b5;
            throw e;
        }
    }
} 
});
MochiKit.Signal.EXPORT_OK = [];
MochiKit.Signal.EXPORT = ["connect", "disconnect", "signal", "disconnectAll", "disconnectAllTo"];
MochiKit.Signal.__new__ = function(win) {
    var m = MochiKit.Base;
    this._document = document;
    this._window = win;
    this._lock = false;
    this._dirty = false;
    try {
        this.connect(window, "onunload", this._unloadCache);
    }
    catch (e) {
    }
    this.EXPORT_TAGS = { ":common": this.EXPORT, ":all": m.concat(this.EXPORT, this.EXPORT_OK) };
    m.nameFunctions(this);
};
MochiKit.Signal.__new__(this);
if (MochiKit.__export__) {
    connect = MochiKit.Signal.connect;
    disconnect = MochiKit.Signal.disconnect;
    disconnectAll = MochiKit.Signal.disconnectAll;
    signal = MochiKit.Signal.signal;
}
MochiKit.Base._exportSymbols(this, MochiKit.Signal);
MochiKit.Base._deps("Position", ["Base", "DOM", "Style"]);
MochiKit.Position.NAME = "MochiKit.Position";
MochiKit.Position.VERSION = "1.4.2";
MochiKit.Position.__repr__ = function() {
    return "[" + this.NAME + " " + this.VERSION + "]";
};
MochiKit.Position.toString = function() {
    return this.__repr__();
};
MochiKit.Position.EXPORT_OK = [];
MochiKit.Position.EXPORT = [];
MochiKit.Base.update(MochiKit.Position, { includeScrollOffsets: false, prepare: function() {
    var _5bb = window.pageXOffset || document.documentElement.scrollLeft || document.body.scrollLeft || 0;
    var _5bc = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0;
    this.windowOffset = new MochiKit.Style.Coordinates(_5bb, _5bc);
}, cumulativeOffset: function(_5bd) {
    var _5be = 0;
    var _5bf = 0;
    do {
        _5be += _5bd.offsetTop || 0;
        _5bf += _5bd.offsetLeft || 0;
        _5bd = _5bd.offsetParent;
    } while (_5bd);
    return new MochiKit.Style.Coordinates(_5bf, _5be);
}, realOffset: function(_5c0) {
    var _5c1 = 0;
    var _5c2 = 0;
    do {
        _5c1 += _5c0.scrollTop || 0;
        _5c2 += _5c0.scrollLeft || 0;
        _5c0 = _5c0.parentNode;
    } while (_5c0);
    return new MochiKit.Style.Coordinates(_5c2, _5c1);
}, within: function(_5c3, x, y) {
    if (this.includeScrollOffsets) {
        return this.withinIncludingScrolloffsets(_5c3, x, y);
    }
    this.xcomp = x;
    this.ycomp = y;
    this.offset = this.cumulativeOffset(_5c3);
    if (_5c3.style.position == "fixed") {
        this.offset.x += this.windowOffset.x;
        this.offset.y += this.windowOffset.y;
    }
    return (y >= this.offset.y && y < this.offset.y + _5c3.offsetHeight && x >= this.offset.x && x < this.offset.x + _5c3.offsetWidth);
}, withinIncludingScrolloffsets: function(_5c6, x, y) {
    var _5c9 = this.realOffset(_5c6);
    this.xcomp = x + _5c9.x - this.windowOffset.x;
    this.ycomp = y + _5c9.y - this.windowOffset.y;
    this.offset = this.cumulativeOffset(_5c6);
    return (this.ycomp >= this.offset.y && this.ycomp < this.offset.y + _5c6.offsetHeight && this.xcomp >= this.offset.x && this.xcomp < this.offset.x + _5c6.offsetWidth);
}, overlap: function(mode, _5cb) {
    if (!mode) {
        return 0;
    }
    if (mode == "vertical") {
        return ((this.offset.y + _5cb.offsetHeight) - this.ycomp) / _5cb.offsetHeight;
    }
    if (mode == "horizontal") {
        return ((this.offset.x + _5cb.offsetWidth) - this.xcomp) / _5cb.offsetWidth;
    }
}, absolutize: function(_5cc) {
    _5cc = MochiKit.DOM.getElement(_5cc);
    if (_5cc.style.position == "absolute") {
        return;
    }
    MochiKit.Position.prepare();
    var _5cd = MochiKit.Position.positionedOffset(_5cc);
    var _5ce = _5cc.clientWidth;
    var _5cf = _5cc.clientHeight;
    var _5d0 = { "position": _5cc.style.position, "left": _5cd.x - parseFloat(_5cc.style.left || 0), "top": _5cd.y - parseFloat(_5cc.style.top || 0), "width": _5cc.style.width, "height": _5cc.style.height };
    _5cc.style.position = "absolute";
    _5cc.style.top = _5cd.y + "px";
    _5cc.style.left = _5cd.x + "px";
    _5cc.style.width = _5ce + "px";
    _5cc.style.height = _5cf + "px";
    return _5d0;
}, positionedOffset: function(_5d1) {
    var _5d2 = 0, _5d3 = 0;
    do {
        _5d2 += _5d1.offsetTop || 0;
        _5d3 += _5d1.offsetLeft || 0;
        _5d1 = _5d1.offsetParent;
        if (_5d1) {
            p = MochiKit.Style.getStyle(_5d1, "position");
            if (p == "relative" || p == "absolute") {
                break;
            }
        }
    } while (_5d1);
    return new MochiKit.Style.Coordinates(_5d3, _5d2);
}, relativize: function(_5d4, _5d5) {
    _5d4 = MochiKit.DOM.getElement(_5d4);
    if (_5d4.style.position == "relative") {
        return;
    }
    MochiKit.Position.prepare();
    var top = parseFloat(_5d4.style.top || 0) - (_5d5["top"] || 0);
    var left = parseFloat(_5d4.style.left || 0) - (_5d5["left"] || 0);
    _5d4.style.position = _5d5["position"];
    _5d4.style.top = top + "px";
    _5d4.style.left = left + "px";
    _5d4.style.width = _5d5["width"];
    _5d4.style.height = _5d5["height"];
}, clone: function(_5d8, _5d9) {
    _5d8 = MochiKit.DOM.getElement(_5d8);
    _5d9 = MochiKit.DOM.getElement(_5d9);
    _5d9.style.position = "absolute";
    var _5da = this.cumulativeOffset(_5d8);
    _5d9.style.top = _5da.y + "px";
    _5d9.style.left = _5da.x + "px";
    _5d9.style.width = _5d8.offsetWidth + "px";
    _5d9.style.height = _5d8.offsetHeight + "px";
}, page: function(_5db) {
    var _5dc = 0;
    var _5dd = 0;
    var _5de = _5db;
    do {
        _5dc += _5de.offsetTop || 0;
        _5dd += _5de.offsetLeft || 0;
        if (_5de.offsetParent == document.body && MochiKit.Style.getStyle(_5de, "position") == "absolute") {
            break;
        }
    } while (_5de = _5de.offsetParent);
    _5de = _5db;
    do {
        _5dc -= _5de.scrollTop || 0;
        _5dd -= _5de.scrollLeft || 0;
    } while (_5de = _5de.parentNode);
    return new MochiKit.Style.Coordinates(_5dd, _5dc);
} 
});
MochiKit.Position.__new__ = function(win) {
    var m = MochiKit.Base;
    this.EXPORT_TAGS = { ":common": this.EXPORT, ":all": m.concat(this.EXPORT, this.EXPORT_OK) };
    m.nameFunctions(this);
};
MochiKit.Position.__new__(this);
MochiKit.Base._exportSymbols(this, MochiKit.Position);
MochiKit.Base._deps("Visual", ["Base", "DOM", "Style", "Color", "Position"]);
MochiKit.Visual.NAME = "MochiKit.Visual";
MochiKit.Visual.VERSION = "1.4.2";
MochiKit.Visual.__repr__ = function() {
    return "[" + this.NAME + " " + this.VERSION + "]";
};
MochiKit.Visual.toString = function() {
    return this.__repr__();
};
MochiKit.Visual._RoundCorners = function(e, _5e2) {
    e = MochiKit.DOM.getElement(e);
    this._setOptions(_5e2);
    if (this.options.__unstable__wrapElement) {
        e = this._doWrap(e);
    }
    var _5e3 = this.options.color;
    var C = MochiKit.Color.Color;
    if (this.options.color === "fromElement") {
        _5e3 = C.fromBackground(e);
    } else {
        if (!(_5e3 instanceof C)) {
            _5e3 = C.fromString(_5e3);
        }
    }
    this.isTransparent = (_5e3.asRGB().a <= 0);
    var _5e5 = this.options.bgColor;
    if (this.options.bgColor === "fromParent") {
        _5e5 = C.fromBackground(e.offsetParent);
    } else {
        if (!(_5e5 instanceof C)) {
            _5e5 = C.fromString(_5e5);
        }
    }
    this._roundCornersImpl(e, _5e3, _5e5);
};
MochiKit.Visual._RoundCorners.prototype = { _doWrap: function(e) {
    var _5e7 = e.parentNode;
    var doc = MochiKit.DOM.currentDocument();
    if (typeof (doc.defaultView) === "undefined" || doc.defaultView === null) {
        return e;
    }
    var _5e9 = doc.defaultView.getComputedStyle(e, null);
    if (typeof (_5e9) === "undefined" || _5e9 === null) {
        return e;
    }
    var _5ea = MochiKit.DOM.DIV({ "style": { display: "block", marginTop: _5e9.getPropertyValue("padding-top"), marginRight: _5e9.getPropertyValue("padding-right"), marginBottom: _5e9.getPropertyValue("padding-bottom"), marginLeft: _5e9.getPropertyValue("padding-left"), padding: "0px"} });
    _5ea.innerHTML = e.innerHTML;
    e.innerHTML = "";
    e.appendChild(_5ea);
    return e;
}, _roundCornersImpl: function(e, _5ec, _5ed) {
    if (this.options.border) {
        this._renderBorder(e, _5ed);
    }
    if (this._isTopRounded()) {
        this._roundTopCorners(e, _5ec, _5ed);
    }
    if (this._isBottomRounded()) {
        this._roundBottomCorners(e, _5ec, _5ed);
    }
}, _renderBorder: function(el, _5ef) {
    var _5f0 = "1px solid " + this._borderColor(_5ef);
    var _5f1 = "border-left: " + _5f0;
    var _5f2 = "border-right: " + _5f0;
    var _5f3 = "style='" + _5f1 + ";" + _5f2 + "'";
    el.innerHTML = "<div " + _5f3 + ">" + el.innerHTML + "</div>";
}, _roundTopCorners: function(el, _5f5, _5f6) {
    var _5f7 = this._createCorner(_5f6);
    for (var i = 0; i < this.options.numSlices; i++) {
        _5f7.appendChild(this._createCornerSlice(_5f5, _5f6, i, "top"));
    }
    el.style.paddingTop = 0;
    el.insertBefore(_5f7, el.firstChild);
}, _roundBottomCorners: function(el, _5fa, _5fb) {
    var _5fc = this._createCorner(_5fb);
    for (var i = (this.options.numSlices - 1); i >= 0; i--) {
        _5fc.appendChild(this._createCornerSlice(_5fa, _5fb, i, "bottom"));
    }
    el.style.paddingBottom = 0;
    el.appendChild(_5fc);
}, _createCorner: function(_5fe) {
    var dom = MochiKit.DOM;
    return dom.DIV({ style: { backgroundColor: _5fe.toString()} });
}, _createCornerSlice: function(_600, _601, n, _603) {
    var _604 = MochiKit.DOM.SPAN();
    var _605 = _604.style;
    _605.backgroundColor = _600.toString();
    _605.display = "block";
    _605.height = "1px";
    _605.overflow = "hidden";
    _605.fontSize = "1px";
    var _606 = this._borderColor(_600, _601);
    if (this.options.border && n === 0) {
        _605.borderTopStyle = "solid";
        _605.borderTopWidth = "1px";
        _605.borderLeftWidth = "0px";
        _605.borderRightWidth = "0px";
        _605.borderBottomWidth = "0px";
        _605.height = "0px";
        _605.borderColor = _606.toString();
    } else {
        if (_606) {
            _605.borderColor = _606.toString();
            _605.borderStyle = "solid";
            _605.borderWidth = "0px 1px";
        }
    }
    if (!this.options.compact && (n == (this.options.numSlices - 1))) {
        _605.height = "2px";
    }
    this._setMargin(_604, n, _603);
    this._setBorder(_604, n, _603);
    return _604;
}, _setOptions: function(_607) {
    this.options = { corners: "all", color: "fromElement", bgColor: "fromParent", blend: true, border: false, compact: false, __unstable__wrapElement: false };
    MochiKit.Base.update(this.options, _607);
    this.options.numSlices = (this.options.compact ? 2 : 4);
}, _whichSideTop: function() {
    var _608 = this.options.corners;
    if (this._hasString(_608, "all", "top")) {
        return "";
    }
    var _609 = (_608.indexOf("tl") != -1);
    var _60a = (_608.indexOf("tr") != -1);
    if (_609 && _60a) {
        return "";
    }
    if (_609) {
        return "left";
    }
    if (_60a) {
        return "right";
    }
    return "";
}, _whichSideBottom: function() {
    var _60b = this.options.corners;
    if (this._hasString(_60b, "all", "bottom")) {
        return "";
    }
    var _60c = (_60b.indexOf("bl") != -1);
    var _60d = (_60b.indexOf("br") != -1);
    if (_60c && _60d) {
        return "";
    }
    if (_60c) {
        return "left";
    }
    if (_60d) {
        return "right";
    }
    return "";
}, _borderColor: function(_60e, _60f) {
    if (_60e == "transparent") {
        return _60f;
    } else {
        if (this.options.border) {
            return this.options.border;
        } else {
            if (this.options.blend) {
                return _60f.blendedColor(_60e);
            }
        }
    }
    return "";
}, _setMargin: function(el, n, _612) {
    var _613 = this._marginSize(n) + "px";
    var _614 = (_612 == "top" ? this._whichSideTop() : this._whichSideBottom());
    var _615 = el.style;
    if (_614 == "left") {
        _615.marginLeft = _613;
        _615.marginRight = "0px";
    } else {
        if (_614 == "right") {
            _615.marginRight = _613;
            _615.marginLeft = "0px";
        } else {
            _615.marginLeft = _613;
            _615.marginRight = _613;
        }
    }
}, _setBorder: function(el, n, _618) {
    var _619 = this._borderSize(n) + "px";
    var _61a = (_618 == "top" ? this._whichSideTop() : this._whichSideBottom());
    var _61b = el.style;
    if (_61a == "left") {
        _61b.borderLeftWidth = _619;
        _61b.borderRightWidth = "0px";
    } else {
        if (_61a == "right") {
            _61b.borderRightWidth = _619;
            _61b.borderLeftWidth = "0px";
        } else {
            _61b.borderLeftWidth = _619;
            _61b.borderRightWidth = _619;
        }
    }
}, _marginSize: function(n) {
    if (this.isTransparent) {
        return 0;
    }
    var o = this.options;
    if (o.compact && o.blend) {
        var _61e = [1, 0];
        return _61e[n];
    } else {
        if (o.compact) {
            var _61f = [2, 1];
            return _61f[n];
        } else {
            if (o.blend) {
                var _620 = [3, 2, 1, 0];
                return _620[n];
            } else {
                var _621 = [5, 3, 2, 1];
                return _621[n];
            }
        }
    }
}, _borderSize: function(n) {
    var o = this.options;
    var _624;
    if (o.compact && (o.blend || this.isTransparent)) {
        return 1;
    } else {
        if (o.compact) {
            _624 = [1, 0];
        } else {
            if (o.blend) {
                _624 = [2, 1, 1, 1];
            } else {
                if (o.border) {
                    _624 = [0, 2, 0, 0];
                } else {
                    if (this.isTransparent) {
                        _624 = [5, 3, 2, 1];
                    } else {
                        return 0;
                    }
                }
            }
        }
    }
    return _624[n];
}, _hasString: function(str) {
    for (var i = 1; i < arguments.length; i++) {
        if (str.indexOf(arguments[i]) != -1) {
            return true;
        }
    }
    return false;
}, _isTopRounded: function() {
    return this._hasString(this.options.corners, "all", "top", "tl", "tr");
}, _isBottomRounded: function() {
    return this._hasString(this.options.corners, "all", "bottom", "bl", "br");
}, _hasSingleTextChild: function(el) {
    return (el.childNodes.length == 1 && el.childNodes[0].nodeType == 3);
} 
};
MochiKit.Visual.roundElement = function(e, _629) {
    new MochiKit.Visual._RoundCorners(e, _629);
};
MochiKit.Visual.roundClass = function(_62a, _62b, _62c) {
    var _62d = MochiKit.DOM.getElementsByTagAndClassName(_62a, _62b);
    for (var i = 0; i < _62d.length; i++) {
        MochiKit.Visual.roundElement(_62d[i], _62c);
    }
};
MochiKit.Visual.tagifyText = function(_62f, _630) {
    _630 = _630 || "position:relative";
    if (/MSIE/.test(navigator.userAgent)) {
        _630 += ";zoom:1";
    }
    _62f = MochiKit.DOM.getElement(_62f);
    var ma = MochiKit.Base.map;
    ma(function(_632) {
        if (_632.nodeType == 3) {
            ma(function(_633) {
                _62f.insertBefore(MochiKit.DOM.SPAN({ style: _630 }, _633 == " " ? String.fromCharCode(160) : _633), _632);
            }, _632.nodeValue.split(""));
            MochiKit.DOM.removeElement(_632);
        }
    }, _62f.childNodes);
};
MochiKit.Visual.forceRerendering = function(_634) {
    try {
        _634 = MochiKit.DOM.getElement(_634);
        var n = document.createTextNode(" ");
        _634.appendChild(n);
        _634.removeChild(n);
    }
    catch (e) {
    }
};
MochiKit.Visual.multiple = function(_636, _637, _638) {
    _638 = MochiKit.Base.update({ speed: 0.1, delay: 0 }, _638);
    var _639 = _638.delay;
    var _63a = 0;
    MochiKit.Base.map(function(_63b) {
        _638.delay = _63a * _638.speed + _639;
        new _637(_63b, _638);
        _63a += 1;
    }, _636);
};
MochiKit.Visual.PAIRS = { "slide": ["slideDown", "slideUp"], "blind": ["blindDown", "blindUp"], "appear": ["appear", "fade"], "size": ["grow", "shrink"] };
MochiKit.Visual.toggle = function(_63c, _63d, _63e) {
    _63c = MochiKit.DOM.getElement(_63c);
    _63d = (_63d || "appear").toLowerCase();
    _63e = MochiKit.Base.update({ queue: { position: "end", scope: (_63c.id || "global"), limit: 1} }, _63e);
    var v = MochiKit.Visual;
    v[MochiKit.Style.getStyle(_63c, "display") != "none" ? v.PAIRS[_63d][1] : v.PAIRS[_63d][0]](_63c, _63e);
};
MochiKit.Visual.Transitions = {};
MochiKit.Visual.Transitions.linear = function(pos) {
    return pos;
};
MochiKit.Visual.Transitions.sinoidal = function(pos) {
    return 0.5 - Math.cos(pos * Math.PI) / 2;
};
MochiKit.Visual.Transitions.reverse = function(pos) {
    return 1 - pos;
};
MochiKit.Visual.Transitions.flicker = function(pos) {
    return 0.25 - Math.cos(pos * Math.PI) / 4 + Math.random() / 2;
};
MochiKit.Visual.Transitions.wobble = function(pos) {
    return 0.5 - Math.cos(9 * pos * Math.PI) / 2;
};
MochiKit.Visual.Transitions.pulse = function(pos, _646) {
    if (_646) {
        pos *= 2 * _646;
    } else {
        pos *= 10;
    }
    var _647 = pos - Math.floor(pos);
    return (Math.floor(pos) % 2 == 0) ? _647 : 1 - _647;
};
MochiKit.Visual.Transitions.parabolic = function(pos) {
    return pos * pos;
};
MochiKit.Visual.Transitions.none = function(pos) {
    return 0;
};
MochiKit.Visual.Transitions.full = function(pos) {
    return 1;
};
MochiKit.Visual.ScopedQueue = function() {
    var cls = arguments.callee;
    if (!(this instanceof cls)) {
        return new cls();
    }
    this.__init__();
};
MochiKit.Base.update(MochiKit.Visual.ScopedQueue.prototype, { __init__: function() {
    this.effects = [];
    this.interval = null;
}, add: function(_64c) {
    var _64d = new Date().getTime();
    var _64e = (typeof (_64c.options.queue) == "string") ? _64c.options.queue : _64c.options.queue.position;
    var ma = MochiKit.Base.map;
    switch (_64e) {
        case "front":
            ma(function(e) {
                if (e.state == "idle") {
                    e.startOn += _64c.finishOn;
                    e.finishOn += _64c.finishOn;
                }
            }, this.effects);
            break;
        case "end":
            var _651;
            ma(function(e) {
                var i = e.finishOn;
                if (i >= (_651 || i)) {
                    _651 = i;
                }
            }, this.effects);
            _64d = _651 || _64d;
            break;
        case "break":
            ma(function(e) {
                e.finalize();
            }, this.effects);
            break;
    }
    _64c.startOn += _64d;
    _64c.finishOn += _64d;
    if (!_64c.options.queue.limit || this.effects.length < _64c.options.queue.limit) {
        this.effects.push(_64c);
    }
    if (!this.interval) {
        this.interval = this.startLoop(MochiKit.Base.bind(this.loop, this), 40);
    }
}, startLoop: function(func, _656) {
    return setInterval(func, _656);
}, remove: function(_657) {
    this.effects = MochiKit.Base.filter(function(e) {
        return e != _657;
    }, this.effects);
    if (!this.effects.length) {
        this.stopLoop(this.interval);
        this.interval = null;
    }
}, stopLoop: function(_659) {
    clearInterval(_659);
}, loop: function() {
    var _65a = new Date().getTime();
    MochiKit.Base.map(function(_65b) {
        _65b.loop(_65a);
    }, this.effects);
} 
});
MochiKit.Visual.Queues = { instances: {}, get: function(_65c) {
    if (typeof (_65c) != "string") {
        return _65c;
    }
    if (!this.instances[_65c]) {
        this.instances[_65c] = new MochiKit.Visual.ScopedQueue();
    }
    return this.instances[_65c];
} 
};
MochiKit.Visual.Queue = MochiKit.Visual.Queues.get("global");
MochiKit.Visual.DefaultOptions = { transition: MochiKit.Visual.Transitions.sinoidal, duration: 1, fps: 25, sync: false, from: 0, to: 1, delay: 0, queue: "parallel" };
MochiKit.Visual.Base = function() {
};
MochiKit.Visual.Base.prototype = { __class__: MochiKit.Visual.Base, start: function(_65d) {
    var v = MochiKit.Visual;
    this.options = MochiKit.Base.setdefault(_65d, v.DefaultOptions);
    this.currentFrame = 0;
    this.state = "idle";
    this.startOn = this.options.delay * 1000;
    this.finishOn = this.startOn + (this.options.duration * 1000);
    this.event("beforeStart");
    if (!this.options.sync) {
        v.Queues.get(typeof (this.options.queue) == "string" ? "global" : this.options.queue.scope).add(this);
    }
}, loop: function(_65f) {
    if (_65f >= this.startOn) {
        if (_65f >= this.finishOn) {
            return this.finalize();
        }
        var pos = (_65f - this.startOn) / (this.finishOn - this.startOn);
        var _661 = Math.round(pos * this.options.fps * this.options.duration);
        if (_661 > this.currentFrame) {
            this.render(pos);
            this.currentFrame = _661;
        }
    }
}, render: function(pos) {
    if (this.state == "idle") {
        this.state = "running";
        this.event("beforeSetup");
        this.setup();
        this.event("afterSetup");
    }
    if (this.state == "running") {
        if (this.options.transition) {
            pos = this.options.transition(pos);
        }
        pos *= (this.options.to - this.options.from);
        pos += this.options.from;
        this.event("beforeUpdate");
        this.update(pos);
        this.event("afterUpdate");
    }
}, cancel: function() {
    if (!this.options.sync) {
        MochiKit.Visual.Queues.get(typeof (this.options.queue) == "string" ? "global" : this.options.queue.scope).remove(this);
    }
    this.state = "finished";
}, finalize: function() {
    this.render(1);
    this.cancel();
    this.event("beforeFinish");
    this.finish();
    this.event("afterFinish");
}, setup: function() {
}, finish: function() {
}, update: function(_663) {
}, event: function(_664) {
    if (this.options[_664 + "Internal"]) {
        this.options[_664 + "Internal"](this);
    }
    if (this.options[_664]) {
        this.options[_664](this);
    }
}, repr: function() {
    return "[" + this.__class__.NAME + ", options:" + MochiKit.Base.repr(this.options) + "]";
} 
};
MochiKit.Visual.Parallel = function(_665, _666) {
    var cls = arguments.callee;
    if (!(this instanceof cls)) {
        return new cls(_665, _666);
    }
    this.__init__(_665, _666);
};
MochiKit.Visual.Parallel.prototype = new MochiKit.Visual.Base();
MochiKit.Base.update(MochiKit.Visual.Parallel.prototype, { __class__: MochiKit.Visual.Parallel, __init__: function(_668, _669) {
    this.effects = _668 || [];
    this.start(_669);
}, update: function(_66a) {
    MochiKit.Base.map(function(_66b) {
        _66b.render(_66a);
    }, this.effects);
}, finish: function() {
    MochiKit.Base.map(function(_66c) {
        _66c.finalize();
    }, this.effects);
} 
});
MochiKit.Visual.Sequence = function(_66d, _66e) {
    var cls = arguments.callee;
    if (!(this instanceof cls)) {
        return new cls(_66d, _66e);
    }
    this.__init__(_66d, _66e);
};
MochiKit.Visual.Sequence.prototype = new MochiKit.Visual.Base();
MochiKit.Base.update(MochiKit.Visual.Sequence.prototype, { __class__: MochiKit.Visual.Sequence, __init__: function(_670, _671) {
    var defs = { transition: MochiKit.Visual.Transitions.linear, duration: 0 };
    this.effects = _670 || [];
    MochiKit.Base.map(function(_673) {
        defs.duration += _673.options.duration;
    }, this.effects);
    MochiKit.Base.setdefault(_671, defs);
    this.start(_671);
}, update: function(_674) {
    var time = _674 * this.options.duration;
    for (var i = 0; i < this.effects.length; i++) {
        var _677 = this.effects[i];
        if (time <= _677.options.duration) {
            _677.render(time / _677.options.duration);
            break;
        } else {
            time -= _677.options.duration;
        }
    }
}, finish: function() {
    MochiKit.Base.map(function(_678) {
        _678.finalize();
    }, this.effects);
} 
});
MochiKit.Visual.Opacity = function(_679, _67a) {
    var cls = arguments.callee;
    if (!(this instanceof cls)) {
        return new cls(_679, _67a);
    }
    this.__init__(_679, _67a);
};
MochiKit.Visual.Opacity.prototype = new MochiKit.Visual.Base();
MochiKit.Base.update(MochiKit.Visual.Opacity.prototype, { __class__: MochiKit.Visual.Opacity, __init__: function(_67c, _67d) {
    var b = MochiKit.Base;
    var s = MochiKit.Style;
    this.element = MochiKit.DOM.getElement(_67c);
    if (this.element.currentStyle && (!this.element.currentStyle.hasLayout)) {
        s.setStyle(this.element, { zoom: 1 });
    }
    _67d = b.update({ from: s.getStyle(this.element, "opacity") || 0, to: 1 }, _67d);
    this.start(_67d);
}, update: function(_680) {
    MochiKit.Style.setStyle(this.element, { "opacity": _680 });
} 
});
MochiKit.Visual.Move = function(_681, _682) {
    var cls = arguments.callee;
    if (!(this instanceof cls)) {
        return new cls(_681, _682);
    }
    this.__init__(_681, _682);
};
MochiKit.Visual.Move.prototype = new MochiKit.Visual.Base();
MochiKit.Base.update(MochiKit.Visual.Move.prototype, { __class__: MochiKit.Visual.Move, __init__: function(_684, _685) {
    this.element = MochiKit.DOM.getElement(_684);
    _685 = MochiKit.Base.update({ x: 0, y: 0, mode: "relative" }, _685);
    this.start(_685);
}, setup: function() {
    MochiKit.Style.makePositioned(this.element);
    var s = this.element.style;
    var _687 = s.visibility;
    var _688 = s.display;
    if (_688 == "none") {
        s.visibility = "hidden";
        s.display = "";
    }
    this.originalLeft = parseFloat(MochiKit.Style.getStyle(this.element, "left") || "0");
    this.originalTop = parseFloat(MochiKit.Style.getStyle(this.element, "top") || "0");
    if (this.options.mode == "absolute") {
        this.options.x -= this.originalLeft;
        this.options.y -= this.originalTop;
    }
    if (_688 == "none") {
        s.visibility = _687;
        s.display = _688;
    }
}, update: function(_689) {
    MochiKit.Style.setStyle(this.element, { left: Math.round(this.options.x * _689 + this.originalLeft) + "px", top: Math.round(this.options.y * _689 + this.originalTop) + "px" });
} 
});
MochiKit.Visual.Scale = function(_68a, _68b, _68c) {
    var cls = arguments.callee;
    if (!(this instanceof cls)) {
        return new cls(_68a, _68b, _68c);
    }
    this.__init__(_68a, _68b, _68c);
};
MochiKit.Visual.Scale.prototype = new MochiKit.Visual.Base();
MochiKit.Base.update(MochiKit.Visual.Scale.prototype, { __class__: MochiKit.Visual.Scale, __init__: function(_68e, _68f, _690) {
    this.element = MochiKit.DOM.getElement(_68e);
    _690 = MochiKit.Base.update({ scaleX: true, scaleY: true, scaleContent: true, scaleFromCenter: false, scaleMode: "box", scaleFrom: 100, scaleTo: _68f }, _690);
    this.start(_690);
}, setup: function() {
    this.restoreAfterFinish = this.options.restoreAfterFinish || false;
    this.elementPositioning = MochiKit.Style.getStyle(this.element, "position");
    var ma = MochiKit.Base.map;
    var b = MochiKit.Base.bind;
    this.originalStyle = {};
    ma(b(function(k) {
        this.originalStyle[k] = this.element.style[k];
    }, this), ["top", "left", "width", "height", "fontSize"]);
    this.originalTop = this.element.offsetTop;
    this.originalLeft = this.element.offsetLeft;
    var _694 = MochiKit.Style.getStyle(this.element, "font-size") || "100%";
    ma(b(function(_695) {
        if (_694.indexOf(_695) > 0) {
            this.fontSize = parseFloat(_694);
            this.fontSizeType = _695;
        }
    }, this), ["em", "px", "%"]);
    this.factor = (this.options.scaleTo - this.options.scaleFrom) / 100;
    if (/^content/.test(this.options.scaleMode)) {
        this.dims = [this.element.scrollHeight, this.element.scrollWidth];
    } else {
        if (this.options.scaleMode == "box") {
            this.dims = [this.element.offsetHeight, this.element.offsetWidth];
        } else {
            this.dims = [this.options.scaleMode.originalHeight, this.options.scaleMode.originalWidth];
        }
    }
}, update: function(_696) {
    var _697 = (this.options.scaleFrom / 100) + (this.factor * _696);
    if (this.options.scaleContent && this.fontSize) {
        MochiKit.Style.setStyle(this.element, { fontSize: this.fontSize * _697 + this.fontSizeType });
    }
    this.setDimensions(this.dims[0] * _697, this.dims[1] * _697);
}, finish: function() {
    if (this.restoreAfterFinish) {
        MochiKit.Style.setStyle(this.element, this.originalStyle);
    }
}, setDimensions: function(_698, _699) {
    var d = {};
    var r = Math.round;
    if (/MSIE/.test(navigator.userAgent)) {
        r = Math.ceil;
    }
    if (this.options.scaleX) {
        d.width = r(_699) + "px";
    }
    if (this.options.scaleY) {
        d.height = r(_698) + "px";
    }
    if (this.options.scaleFromCenter) {
        var topd = (_698 - this.dims[0]) / 2;
        var _69d = (_699 - this.dims[1]) / 2;
        if (this.elementPositioning == "absolute") {
            if (this.options.scaleY) {
                d.top = this.originalTop - topd + "px";
            }
            if (this.options.scaleX) {
                d.left = this.originalLeft - _69d + "px";
            }
        } else {
            if (this.options.scaleY) {
                d.top = -topd + "px";
            }
            if (this.options.scaleX) {
                d.left = -_69d + "px";
            }
        }
    }
    MochiKit.Style.setStyle(this.element, d);
} 
});
MochiKit.Visual.Highlight = function(_69e, _69f) {
    var cls = arguments.callee;
    if (!(this instanceof cls)) {
        return new cls(_69e, _69f);
    }
    this.__init__(_69e, _69f);
};
MochiKit.Visual.Highlight.prototype = new MochiKit.Visual.Base();
MochiKit.Base.update(MochiKit.Visual.Highlight.prototype, { __class__: MochiKit.Visual.Highlight, __init__: function(_6a1, _6a2) {
    this.element = MochiKit.DOM.getElement(_6a1);
    _6a2 = MochiKit.Base.update({ startcolor: "#ffff99" }, _6a2);
    this.start(_6a2);
}, setup: function() {
    var b = MochiKit.Base;
    var s = MochiKit.Style;
    if (s.getStyle(this.element, "display") == "none") {
        this.cancel();
        return;
    }
    this.oldStyle = { backgroundImage: s.getStyle(this.element, "background-image") };
    s.setStyle(this.element, { backgroundImage: "none" });
    if (!this.options.endcolor) {
        this.options.endcolor = MochiKit.Color.Color.fromBackground(this.element).toHexString();
    }
    if (b.isUndefinedOrNull(this.options.restorecolor)) {
        this.options.restorecolor = s.getStyle(this.element, "background-color");
    }
    this._base = b.map(b.bind(function(i) {
        return parseInt(this.options.startcolor.slice(i * 2 + 1, i * 2 + 3), 16);
    }, this), [0, 1, 2]);
    this._delta = b.map(b.bind(function(i) {
        return parseInt(this.options.endcolor.slice(i * 2 + 1, i * 2 + 3), 16) - this._base[i];
    }, this), [0, 1, 2]);
}, update: function(_6a7) {
    var m = "#";
    MochiKit.Base.map(MochiKit.Base.bind(function(i) {
        m += MochiKit.Color.toColorPart(Math.round(this._base[i] + this._delta[i] * _6a7));
    }, this), [0, 1, 2]);
    MochiKit.Style.setStyle(this.element, { backgroundColor: m });
}, finish: function() {
    MochiKit.Style.setStyle(this.element, MochiKit.Base.update(this.oldStyle, { backgroundColor: this.options.restorecolor }));
} 
});
MochiKit.Visual.ScrollTo = function(_6aa, _6ab) {
    var cls = arguments.callee;
    if (!(this instanceof cls)) {
        return new cls(_6aa, _6ab);
    }
    this.__init__(_6aa, _6ab);
};
MochiKit.Visual.ScrollTo.prototype = new MochiKit.Visual.Base();
MochiKit.Base.update(MochiKit.Visual.ScrollTo.prototype, { __class__: MochiKit.Visual.ScrollTo, __init__: function(_6ad, _6ae) {
    this.element = MochiKit.DOM.getElement(_6ad);
    this.start(_6ae);
}, setup: function() {
    var p = MochiKit.Position;
    p.prepare();
    var _6b0 = p.cumulativeOffset(this.element);
    if (this.options.offset) {
        _6b0.y += this.options.offset;
    }
    var max;
    if (window.innerHeight) {
        max = window.innerHeight - window.height;
    } else {
        if (document.documentElement && document.documentElement.clientHeight) {
            max = document.documentElement.clientHeight - document.body.scrollHeight;
        } else {
            if (document.body) {
                max = document.body.clientHeight - document.body.scrollHeight;
            }
        }
    }
    this.scrollStart = p.windowOffset.y;
    this.delta = (_6b0.y > max ? max : _6b0.y) - this.scrollStart;
}, update: function(_6b2) {
    var p = MochiKit.Position;
    p.prepare();
    window.scrollTo(p.windowOffset.x, this.scrollStart + (_6b2 * this.delta));
} 
});
MochiKit.Visual.CSS_LENGTH = /^(([\+\-]?[0-9\.]+)(em|ex|px|in|cm|mm|pt|pc|\%))|0$/;
MochiKit.Visual.Morph = function(_6b4, _6b5) {
    var cls = arguments.callee;
    if (!(this instanceof cls)) {
        return new cls(_6b4, _6b5);
    }
    this.__init__(_6b4, _6b5);
};
MochiKit.Visual.Morph.prototype = new MochiKit.Visual.Base();
MochiKit.Base.update(MochiKit.Visual.Morph.prototype, { __class__: MochiKit.Visual.Morph, __init__: function(_6b7, _6b8) {
    this.element = MochiKit.DOM.getElement(_6b7);
    this.start(_6b8);
}, setup: function() {
    var b = MochiKit.Base;
    var _6ba = this.options.style;
    this.styleStart = {};
    this.styleEnd = {};
    this.units = {};
    var _6bb, unit;
    for (var s in _6ba) {
        _6bb = _6ba[s];
        s = b.camelize(s);
        if (MochiKit.Visual.CSS_LENGTH.test(_6bb)) {
            var _6be = _6bb.match(/^([\+\-]?[0-9\.]+)(.*)$/);
            _6bb = parseFloat(_6be[1]);
            unit = (_6be.length == 3) ? _6be[2] : null;
            this.styleEnd[s] = _6bb;
            this.units[s] = unit;
            _6bb = MochiKit.Style.getStyle(this.element, s);
            _6be = _6bb.match(/^([\+\-]?[0-9\.]+)(.*)$/);
            _6bb = parseFloat(_6be[1]);
            this.styleStart[s] = _6bb;
        } else {
            if (/[Cc]olor$/.test(s)) {
                var c = MochiKit.Color.Color;
                _6bb = c.fromString(_6bb);
                if (_6bb) {
                    this.units[s] = "color";
                    this.styleEnd[s] = _6bb.toHexString();
                    _6bb = MochiKit.Style.getStyle(this.element, s);
                    this.styleStart[s] = c.fromString(_6bb).toHexString();
                    this.styleStart[s] = b.map(b.bind(function(i) {
                        return parseInt(this.styleStart[s].slice(i * 2 + 1, i * 2 + 3), 16);
                    }, this), [0, 1, 2]);
                    this.styleEnd[s] = b.map(b.bind(function(i) {
                        return parseInt(this.styleEnd[s].slice(i * 2 + 1, i * 2 + 3), 16);
                    }, this), [0, 1, 2]);
                }
            } else {
                this.element.style[s] = _6bb;
            }
        }
    }
}, update: function(_6c2) {
    var _6c3;
    for (var s in this.styleStart) {
        if (this.units[s] == "color") {
            var m = "#";
            var _6c6 = this.styleStart[s];
            var end = this.styleEnd[s];
            MochiKit.Base.map(MochiKit.Base.bind(function(i) {
                m += MochiKit.Color.toColorPart(Math.round(_6c6[i] + (end[i] - _6c6[i]) * _6c2));
            }, this), [0, 1, 2]);
            this.element.style[s] = m;
        } else {
            _6c3 = this.styleStart[s] + Math.round((this.styleEnd[s] - this.styleStart[s]) * _6c2 * 1000) / 1000 + this.units[s];
            this.element.style[s] = _6c3;
        }
    }
} 
});
MochiKit.Visual.fade = function(_6c9, _6ca) {
    var s = MochiKit.Style;
    var _6cc = s.getStyle(_6c9, "opacity");
    _6ca = MochiKit.Base.update({ from: s.getStyle(_6c9, "opacity") || 1, to: 0, afterFinishInternal: function(_6cd) {
        if (_6cd.options.to !== 0) {
            return;
        }
        s.hideElement(_6cd.element);
        s.setStyle(_6cd.element, { "opacity": _6cc });
    } 
    }, _6ca);
    return new MochiKit.Visual.Opacity(_6c9, _6ca);
};
MochiKit.Visual.appear = function(_6ce, _6cf) {
    var s = MochiKit.Style;
    var v = MochiKit.Visual;
    _6cf = MochiKit.Base.update({ from: (s.getStyle(_6ce, "display") == "none" ? 0 : s.getStyle(_6ce, "opacity") || 0), to: 1, afterFinishInternal: function(_6d2) {
        v.forceRerendering(_6d2.element);
    }, beforeSetupInternal: function(_6d3) {
        s.setStyle(_6d3.element, { "opacity": _6d3.options.from });
        s.showElement(_6d3.element);
    } 
    }, _6cf);
    return new v.Opacity(_6ce, _6cf);
};
MochiKit.Visual.puff = function(_6d4, _6d5) {
    var s = MochiKit.Style;
    var v = MochiKit.Visual;
    _6d4 = MochiKit.DOM.getElement(_6d4);
    var _6d8 = MochiKit.Style.getElementDimensions(_6d4, true);
    var _6d9 = { position: s.getStyle(_6d4, "position"), top: _6d4.style.top, left: _6d4.style.left, width: _6d4.style.width, height: _6d4.style.height, opacity: s.getStyle(_6d4, "opacity") };
    _6d5 = MochiKit.Base.update({ beforeSetupInternal: function(_6da) {
        MochiKit.Position.absolutize(_6da.effects[0].element);
    }, afterFinishInternal: function(_6db) {
        s.hideElement(_6db.effects[0].element);
        s.setStyle(_6db.effects[0].element, _6d9);
    }, scaleContent: true, scaleFromCenter: true
    }, _6d5);
    return new v.Parallel([new v.Scale(_6d4, 200, { sync: true, scaleFromCenter: _6d5.scaleFromCenter, scaleMode: { originalHeight: _6d8.h, originalWidth: _6d8.w }, scaleContent: _6d5.scaleContent, restoreAfterFinish: true }), new v.Opacity(_6d4, { sync: true, to: 0 })], _6d5);
};
MochiKit.Visual.blindUp = function(_6dc, _6dd) {
    var d = MochiKit.DOM;
    var s = MochiKit.Style;
    _6dc = d.getElement(_6dc);
    var _6e0 = s.getElementDimensions(_6dc, true);
    var _6e1 = s.makeClipping(_6dc);
    _6dd = MochiKit.Base.update({ scaleContent: false, scaleX: false, scaleMode: { originalHeight: _6e0.h, originalWidth: _6e0.w }, restoreAfterFinish: true, afterFinishInternal: function(_6e2) {
        s.hideElement(_6e2.element);
        s.undoClipping(_6e2.element, _6e1);
    } 
    }, _6dd);
    return new MochiKit.Visual.Scale(_6dc, 0, _6dd);
};
MochiKit.Visual.blindDown = function(_6e3, _6e4) {
    var d = MochiKit.DOM;
    var s = MochiKit.Style;
    _6e3 = d.getElement(_6e3);
    var _6e7 = s.getElementDimensions(_6e3, true);
    var _6e8;
    _6e4 = MochiKit.Base.update({ scaleContent: false, scaleX: false, scaleFrom: 0, scaleMode: { originalHeight: _6e7.h, originalWidth: _6e7.w }, restoreAfterFinish: true, afterSetupInternal: function(_6e9) {
        _6e8 = s.makeClipping(_6e9.element);
        s.setStyle(_6e9.element, { height: "0px" });
        s.showElement(_6e9.element);
    }, afterFinishInternal: function(_6ea) {
        s.undoClipping(_6ea.element, _6e8);
    } 
    }, _6e4);
    return new MochiKit.Visual.Scale(_6e3, 100, _6e4);
};
MochiKit.Visual.switchOff = function(_6eb, _6ec) {
    var d = MochiKit.DOM;
    var s = MochiKit.Style;
    _6eb = d.getElement(_6eb);
    var _6ef = s.getElementDimensions(_6eb, true);
    var _6f0 = s.getStyle(_6eb, "opacity");
    var _6f1;
    _6ec = MochiKit.Base.update({ duration: 0.7, restoreAfterFinish: true, beforeSetupInternal: function(_6f2) {
        s.makePositioned(_6eb);
        _6f1 = s.makeClipping(_6eb);
    }, afterFinishInternal: function(_6f3) {
        s.hideElement(_6eb);
        s.undoClipping(_6eb, _6f1);
        s.undoPositioned(_6eb);
        s.setStyle(_6eb, { "opacity": _6f0 });
    } 
    }, _6ec);
    var v = MochiKit.Visual;
    return new v.Sequence([new v.appear(_6eb, { sync: true, duration: 0.57 * _6ec.duration, from: 0, transition: v.Transitions.flicker }), new v.Scale(_6eb, 1, { sync: true, duration: 0.43 * _6ec.duration, scaleFromCenter: true, scaleX: false, scaleMode: { originalHeight: _6ef.h, originalWidth: _6ef.w }, scaleContent: false, restoreAfterFinish: true })], _6ec);
};
MochiKit.Visual.dropOut = function(_6f5, _6f6) {
    var d = MochiKit.DOM;
    var s = MochiKit.Style;
    _6f5 = d.getElement(_6f5);
    var _6f9 = { top: s.getStyle(_6f5, "top"), left: s.getStyle(_6f5, "left"), opacity: s.getStyle(_6f5, "opacity") };
    _6f6 = MochiKit.Base.update({ duration: 0.5, distance: 100, beforeSetupInternal: function(_6fa) {
        s.makePositioned(_6fa.effects[0].element);
    }, afterFinishInternal: function(_6fb) {
        s.hideElement(_6fb.effects[0].element);
        s.undoPositioned(_6fb.effects[0].element);
        s.setStyle(_6fb.effects[0].element, _6f9);
    } 
    }, _6f6);
    var v = MochiKit.Visual;
    return new v.Parallel([new v.Move(_6f5, { x: 0, y: _6f6.distance, sync: true }), new v.Opacity(_6f5, { sync: true, to: 0 })], _6f6);
};
MochiKit.Visual.shake = function(_6fd, _6fe) {
    var d = MochiKit.DOM;
    var v = MochiKit.Visual;
    var s = MochiKit.Style;
    _6fd = d.getElement(_6fd);
    var _702 = { top: s.getStyle(_6fd, "top"), left: s.getStyle(_6fd, "left") };
    _6fe = MochiKit.Base.update({ duration: 0.5, afterFinishInternal: function(_703) {
        s.undoPositioned(_6fd);
        s.setStyle(_6fd, _702);
    } 
    }, _6fe);
    return new v.Sequence([new v.Move(_6fd, { sync: true, duration: 0.1 * _6fe.duration, x: 20, y: 0 }), new v.Move(_6fd, { sync: true, duration: 0.2 * _6fe.duration, x: -40, y: 0 }), new v.Move(_6fd, { sync: true, duration: 0.2 * _6fe.duration, x: 40, y: 0 }), new v.Move(_6fd, { sync: true, duration: 0.2 * _6fe.duration, x: -40, y: 0 }), new v.Move(_6fd, { sync: true, duration: 0.2 * _6fe.duration, x: 40, y: 0 }), new v.Move(_6fd, { sync: true, duration: 0.1 * _6fe.duration, x: -20, y: 0 })], _6fe);
};
MochiKit.Visual.slideDown = function(_704, _705) {
    var d = MochiKit.DOM;
    var b = MochiKit.Base;
    var s = MochiKit.Style;
    _704 = d.getElement(_704);
    if (!_704.firstChild) {
        throw new Error("MochiKit.Visual.slideDown must be used on a element with a child");
    }
    d.removeEmptyTextNodes(_704);
    var _709 = s.getStyle(_704.firstChild, "bottom") || 0;
    var _70a = s.getElementDimensions(_704, true);
    var _70b;
    _705 = b.update({ scaleContent: false, scaleX: false, scaleFrom: 0, scaleMode: { originalHeight: _70a.h, originalWidth: _70a.w }, restoreAfterFinish: true, afterSetupInternal: function(_70c) {
        s.makePositioned(_70c.element);
        s.makePositioned(_70c.element.firstChild);
        if (/Opera/.test(navigator.userAgent)) {
            s.setStyle(_70c.element, { top: "" });
        }
        _70b = s.makeClipping(_70c.element);
        s.setStyle(_70c.element, { height: "0px" });
        s.showElement(_70c.element);
    }, afterUpdateInternal: function(_70d) {
        var _70e = s.getElementDimensions(_70d.element, true);
        s.setStyle(_70d.element.firstChild, { bottom: (_70d.dims[0] - _70e.h) + "px" });
    }, afterFinishInternal: function(_70f) {
        s.undoClipping(_70f.element, _70b);
        if (/MSIE/.test(navigator.userAgent)) {
            s.undoPositioned(_70f.element);
            s.undoPositioned(_70f.element.firstChild);
        } else {
            s.undoPositioned(_70f.element.firstChild);
            s.undoPositioned(_70f.element);
        }
        s.setStyle(_70f.element.firstChild, { bottom: _709 });
    } 
    }, _705);
    return new MochiKit.Visual.Scale(_704, 100, _705);
};
MochiKit.Visual.slideUp = function(_710, _711) {
    var d = MochiKit.DOM;
    var b = MochiKit.Base;
    var s = MochiKit.Style;
    _710 = d.getElement(_710);
    if (!_710.firstChild) {
        throw new Error("MochiKit.Visual.slideUp must be used on a element with a child");
    }
    d.removeEmptyTextNodes(_710);
    var _715 = s.getStyle(_710.firstChild, "bottom");
    var _716 = s.getElementDimensions(_710, true);
    var _717;
    _711 = b.update({ scaleContent: false, scaleX: false, scaleMode: { originalHeight: _716.h, originalWidth: _716.w }, scaleFrom: 100, restoreAfterFinish: true, beforeStartInternal: function(_718) {
        s.makePositioned(_718.element);
        s.makePositioned(_718.element.firstChild);
        if (/Opera/.test(navigator.userAgent)) {
            s.setStyle(_718.element, { top: "" });
        }
        _717 = s.makeClipping(_718.element);
        s.showElement(_718.element);
    }, afterUpdateInternal: function(_719) {
        var _71a = s.getElementDimensions(_719.element, true);
        s.setStyle(_719.element.firstChild, { bottom: (_719.dims[0] - _71a.h) + "px" });
    }, afterFinishInternal: function(_71b) {
        s.hideElement(_71b.element);
        s.undoClipping(_71b.element, _717);
        s.undoPositioned(_71b.element.firstChild);
        s.undoPositioned(_71b.element);
        s.setStyle(_71b.element.firstChild, { bottom: _715 });
    } 
    }, _711);
    return new MochiKit.Visual.Scale(_710, 0, _711);
};
MochiKit.Visual.squish = function(_71c, _71d) {
    var d = MochiKit.DOM;
    var b = MochiKit.Base;
    var s = MochiKit.Style;
    var _721 = s.getElementDimensions(_71c, true);
    var _722;
    _71d = b.update({ restoreAfterFinish: true, scaleMode: { originalHeight: _721.w, originalWidth: _721.h }, beforeSetupInternal: function(_723) {
        _722 = s.makeClipping(_723.element);
    }, afterFinishInternal: function(_724) {
        s.hideElement(_724.element);
        s.undoClipping(_724.element, _722);
    } 
    }, _71d);
    return new MochiKit.Visual.Scale(_71c, /Opera/.test(navigator.userAgent) ? 1 : 0, _71d);
};
MochiKit.Visual.grow = function(_725, _726) {
    var d = MochiKit.DOM;
    var v = MochiKit.Visual;
    var s = MochiKit.Style;
    _725 = d.getElement(_725);
    _726 = MochiKit.Base.update({ direction: "center", moveTransition: v.Transitions.sinoidal, scaleTransition: v.Transitions.sinoidal, opacityTransition: v.Transitions.full, scaleContent: true, scaleFromCenter: false }, _726);
    var _72a = { top: _725.style.top, left: _725.style.left, height: _725.style.height, width: _725.style.width, opacity: s.getStyle(_725, "opacity") };
    var dims = s.getElementDimensions(_725, true);
    var _72c, _72d;
    var _72e, _72f;
    switch (_726.direction) {
        case "top-left":
            _72c = _72d = _72e = _72f = 0;
            break;
        case "top-right":
            _72c = dims.w;
            _72d = _72f = 0;
            _72e = -dims.w;
            break;
        case "bottom-left":
            _72c = _72e = 0;
            _72d = dims.h;
            _72f = -dims.h;
            break;
        case "bottom-right":
            _72c = dims.w;
            _72d = dims.h;
            _72e = -dims.w;
            _72f = -dims.h;
            break;
        case "center":
            _72c = dims.w / 2;
            _72d = dims.h / 2;
            _72e = -dims.w / 2;
            _72f = -dims.h / 2;
            break;
    }
    var _730 = MochiKit.Base.update({ beforeSetupInternal: function(_731) {
        s.setStyle(_731.effects[0].element, { height: "0px" });
        s.showElement(_731.effects[0].element);
    }, afterFinishInternal: function(_732) {
        s.undoClipping(_732.effects[0].element);
        s.undoPositioned(_732.effects[0].element);
        s.setStyle(_732.effects[0].element, _72a);
    } 
    }, _726);
    return new v.Move(_725, { x: _72c, y: _72d, duration: 0.01, beforeSetupInternal: function(_733) {
        s.hideElement(_733.element);
        s.makeClipping(_733.element);
        s.makePositioned(_733.element);
    }, afterFinishInternal: function(_734) {
        new v.Parallel([new v.Opacity(_734.element, { sync: true, to: 1, from: 0, transition: _726.opacityTransition }), new v.Move(_734.element, { x: _72e, y: _72f, sync: true, transition: _726.moveTransition }), new v.Scale(_734.element, 100, { scaleMode: { originalHeight: dims.h, originalWidth: dims.w }, sync: true, scaleFrom: /Opera/.test(navigator.userAgent) ? 1 : 0, transition: _726.scaleTransition, scaleContent: _726.scaleContent, scaleFromCenter: _726.scaleFromCenter, restoreAfterFinish: true })], _730);
    } 
    });
};
MochiKit.Visual.shrink = function(_735, _736) {
    var d = MochiKit.DOM;
    var v = MochiKit.Visual;
    var s = MochiKit.Style;
    _735 = d.getElement(_735);
    _736 = MochiKit.Base.update({ direction: "center", moveTransition: v.Transitions.sinoidal, scaleTransition: v.Transitions.sinoidal, opacityTransition: v.Transitions.none, scaleContent: true, scaleFromCenter: false }, _736);
    var _73a = { top: _735.style.top, left: _735.style.left, height: _735.style.height, width: _735.style.width, opacity: s.getStyle(_735, "opacity") };
    var dims = s.getElementDimensions(_735, true);
    var _73c, _73d;
    switch (_736.direction) {
        case "top-left":
            _73c = _73d = 0;
            break;
        case "top-right":
            _73c = dims.w;
            _73d = 0;
            break;
        case "bottom-left":
            _73c = 0;
            _73d = dims.h;
            break;
        case "bottom-right":
            _73c = dims.w;
            _73d = dims.h;
            break;
        case "center":
            _73c = dims.w / 2;
            _73d = dims.h / 2;
            break;
    }
    var _73e;
    var _73f = MochiKit.Base.update({ beforeStartInternal: function(_740) {
        s.makePositioned(_740.effects[0].element);
        _73e = s.makeClipping(_740.effects[0].element);
    }, afterFinishInternal: function(_741) {
        s.hideElement(_741.effects[0].element);
        s.undoClipping(_741.effects[0].element, _73e);
        s.undoPositioned(_741.effects[0].element);
        s.setStyle(_741.effects[0].element, _73a);
    } 
    }, _736);
    return new v.Parallel([new v.Opacity(_735, { sync: true, to: 0, from: 1, transition: _736.opacityTransition }), new v.Scale(_735, /Opera/.test(navigator.userAgent) ? 1 : 0, { scaleMode: { originalHeight: dims.h, originalWidth: dims.w }, sync: true, transition: _736.scaleTransition, scaleContent: _736.scaleContent, scaleFromCenter: _736.scaleFromCenter, restoreAfterFinish: true }), new v.Move(_735, { x: _73c, y: _73d, sync: true, transition: _736.moveTransition })], _73f);
};
MochiKit.Visual.pulsate = function(_742, _743) {
    var d = MochiKit.DOM;
    var v = MochiKit.Visual;
    var b = MochiKit.Base;
    var _747 = MochiKit.Style.getStyle(_742, "opacity");
    _743 = b.update({ duration: 3, from: 0, afterFinishInternal: function(_748) {
        MochiKit.Style.setStyle(_748.element, { "opacity": _747 });
    } 
    }, _743);
    var _749 = _743.transition || v.Transitions.sinoidal;
    _743.transition = function(pos) {
        return _749(1 - v.Transitions.pulse(pos, _743.pulses));
    };
    return new v.Opacity(_742, _743);
};
MochiKit.Visual.fold = function(_74b, _74c) {
    var d = MochiKit.DOM;
    var v = MochiKit.Visual;
    var s = MochiKit.Style;
    _74b = d.getElement(_74b);
    var _750 = s.getElementDimensions(_74b, true);
    var _751 = { top: _74b.style.top, left: _74b.style.left, width: _74b.style.width, height: _74b.style.height };
    var _752 = s.makeClipping(_74b);
    _74c = MochiKit.Base.update({ scaleContent: false, scaleX: false, scaleMode: { originalHeight: _750.h, originalWidth: _750.w }, afterFinishInternal: function(_753) {
        new v.Scale(_74b, 1, { scaleContent: false, scaleY: false, scaleMode: { originalHeight: _750.h, originalWidth: _750.w }, afterFinishInternal: function(_754) {
            s.hideElement(_754.element);
            s.undoClipping(_754.element, _752);
            s.setStyle(_754.element, _751);
        } 
        });
    } 
    }, _74c);
    return new v.Scale(_74b, 5, _74c);
};
MochiKit.Visual.Color = MochiKit.Color.Color;
MochiKit.Visual.getElementsComputedStyle = MochiKit.DOM.computedStyle;
MochiKit.Visual.__new__ = function() {
    var m = MochiKit.Base;
    m.nameFunctions(this);
    this.EXPORT_TAGS = { ":common": this.EXPORT, ":all": m.concat(this.EXPORT, this.EXPORT_OK) };
};
MochiKit.Visual.EXPORT = ["roundElement", "roundClass", "tagifyText", "multiple", "toggle", "Parallel", "Sequence", "Opacity", "Move", "Scale", "Highlight", "ScrollTo", "Morph", "fade", "appear", "puff", "blindUp", "blindDown", "switchOff", "dropOut", "shake", "slideDown", "slideUp", "squish", "grow", "shrink", "pulsate", "fold"];
MochiKit.Visual.EXPORT_OK = ["Base", "PAIRS"];
MochiKit.Visual.__new__();
MochiKit.Base._exportSymbols(this, MochiKit.Visual);
MochiKit.Base._deps("DragAndDrop", ["Base", "Iter", "DOM", "Signal", "Visual", "Position"]);
MochiKit.DragAndDrop.NAME = "MochiKit.DragAndDrop";
MochiKit.DragAndDrop.VERSION = "1.4.2";
MochiKit.DragAndDrop.__repr__ = function() {
    return "[" + this.NAME + " " + this.VERSION + "]";
};
MochiKit.DragAndDrop.toString = function() {
    return this.__repr__();
};
MochiKit.DragAndDrop.EXPORT = ["Droppable", "Draggable"];
MochiKit.DragAndDrop.EXPORT_OK = ["Droppables", "Draggables"];
MochiKit.DragAndDrop.Droppables = { drops: [], remove: function(_756) {
    this.drops = MochiKit.Base.filter(function(d) {
        return d.element != MochiKit.DOM.getElement(_756);
    }, this.drops);
}, register: function(drop) {
    this.drops.push(drop);
}, unregister: function(drop) {
    this.drops = MochiKit.Base.filter(function(d) {
        return d != drop;
    }, this.drops);
}, prepare: function(_75b) {
    MochiKit.Base.map(function(drop) {
        if (drop.isAccepted(_75b)) {
            if (drop.options.activeclass) {
                MochiKit.DOM.addElementClass(drop.element, drop.options.activeclass);
            }
            drop.options.onactive(drop.element, _75b);
        }
    }, this.drops);
}, findDeepestChild: function(_75d) {
    deepest = _75d[0];
    for (i = 1; i < _75d.length; ++i) {
        if (MochiKit.DOM.isChildNode(_75d[i].element, deepest.element)) {
            deepest = _75d[i];
        }
    }
    return deepest;
}, show: function(_75e, _75f) {
    if (!this.drops.length) {
        return;
    }
    var _760 = [];
    if (this.last_active) {
        this.last_active.deactivate();
    }
    MochiKit.Iter.forEach(this.drops, function(drop) {
        if (drop.isAffected(_75e, _75f)) {
            _760.push(drop);
        }
    });
    if (_760.length > 0) {
        drop = this.findDeepestChild(_760);
        MochiKit.Position.within(drop.element, _75e.page.x, _75e.page.y);
        drop.options.onhover(_75f, drop.element, MochiKit.Position.overlap(drop.options.overlap, drop.element));
        drop.activate();
    }
}, fire: function(_762, _763) {
    if (!this.last_active) {
        return;
    }
    MochiKit.Position.prepare();
    if (this.last_active.isAffected(_762.mouse(), _763)) {
        this.last_active.options.ondrop(_763, this.last_active.element, _762);
    }
}, reset: function(_764) {
    MochiKit.Base.map(function(drop) {
        if (drop.options.activeclass) {
            MochiKit.DOM.removeElementClass(drop.element, drop.options.activeclass);
        }
        drop.options.ondesactive(drop.element, _764);
    }, this.drops);
    if (this.last_active) {
        this.last_active.deactivate();
    }
} 
};
MochiKit.DragAndDrop.Droppable = function(_766, _767) {
    var cls = arguments.callee;
    if (!(this instanceof cls)) {
        return new cls(_766, _767);
    }
    this.__init__(_766, _767);
};
MochiKit.DragAndDrop.Droppable.prototype = { __class__: MochiKit.DragAndDrop.Droppable, __init__: function(_769, _76a) {
    var d = MochiKit.DOM;
    var b = MochiKit.Base;
    this.element = d.getElement(_769);
    this.options = b.update({ greedy: true, hoverclass: null, activeclass: null, hoverfunc: b.noop, accept: null, onactive: b.noop, ondesactive: b.noop, onhover: b.noop, ondrop: b.noop, containment: [], tree: false }, _76a);
    this.options._containers = [];
    b.map(MochiKit.Base.bind(function(c) {
        this.options._containers.push(d.getElement(c));
    }, this), this.options.containment);
    MochiKit.Style.makePositioned(this.element);
    MochiKit.DragAndDrop.Droppables.register(this);
}, isContained: function(_76e) {
    if (this.options._containers.length) {
        var _76f;
        if (this.options.tree) {
            _76f = _76e.treeNode;
        } else {
            _76f = _76e.parentNode;
        }
        return MochiKit.Iter.some(this.options._containers, function(c) {
            return _76f == c;
        });
    } else {
        return true;
    }
}, isAccepted: function(_771) {
    return ((!this.options.accept) || MochiKit.Iter.some(this.options.accept, function(c) {
        return MochiKit.DOM.hasElementClass(_771, c);
    }));
}, isAffected: function(_773, _774) {
    return ((this.element != _774) && this.isContained(_774) && this.isAccepted(_774) && MochiKit.Position.within(this.element, _773.page.x, _773.page.y));
}, deactivate: function() {
    if (this.options.hoverclass) {
        MochiKit.DOM.removeElementClass(this.element, this.options.hoverclass);
    }
    this.options.hoverfunc(this.element, false);
    MochiKit.DragAndDrop.Droppables.last_active = null;
}, activate: function() {
    if (this.options.hoverclass) {
        MochiKit.DOM.addElementClass(this.element, this.options.hoverclass);
    }
    this.options.hoverfunc(this.element, true);
    MochiKit.DragAndDrop.Droppables.last_active = this;
}, destroy: function() {
    MochiKit.DragAndDrop.Droppables.unregister(this);
}, repr: function() {
    return "[" + this.__class__.NAME + ", options:" + MochiKit.Base.repr(this.options) + "]";
} 
};
MochiKit.DragAndDrop.Draggables = { drags: [], register: function(_775) {
    if (this.drags.length === 0) {
        var conn = MochiKit.Signal.connect;
        this.eventMouseUp = conn(document, "onmouseup", this, this.endDrag);
        this.eventMouseMove = conn(document, "onmousemove", this, this.updateDrag);
        this.eventKeypress = conn(document, "onkeypress", this, this.keyPress);
    }
    this.drags.push(_775);
}, unregister: function(_777) {
    this.drags = MochiKit.Base.filter(function(d) {
        return d != _777;
    }, this.drags);
    if (this.drags.length === 0) {
        var disc = MochiKit.Signal.disconnect;
        disc(this.eventMouseUp);
        disc(this.eventMouseMove);
        disc(this.eventKeypress);
    }
}, activate: function(_77a) {
    window.focus();
    this.activeDraggable = _77a;
}, deactivate: function() {
    this.activeDraggable = null;
}, updateDrag: function(_77b) {
    if (!this.activeDraggable) {
        return;
    }
    var _77c = _77b.mouse();
    if (this._lastPointer && (MochiKit.Base.repr(this._lastPointer.page) == MochiKit.Base.repr(_77c.page))) {
        return;
    }
    this._lastPointer = _77c;
    this.activeDraggable.updateDrag(_77b, _77c);
}, endDrag: function(_77d) {
    if (!this.activeDraggable) {
        return;
    }
    this._lastPointer = null;
    this.activeDraggable.endDrag(_77d);
    this.activeDraggable = null;
}, keyPress: function(_77e) {
    if (this.activeDraggable) {
        this.activeDraggable.keyPress(_77e);
    }
}, notify: function(_77f, _780, _781) {
    MochiKit.Signal.signal(this, _77f, _780, _781);
} 
};
MochiKit.DragAndDrop.Draggable = function(_782, _783) {
    var cls = arguments.callee;
    if (!(this instanceof cls)) {
        return new cls(_782, _783);
    }
    this.__init__(_782, _783);
};
MochiKit.DragAndDrop.Draggable.prototype = { __class__: MochiKit.DragAndDrop.Draggable, __init__: function(_785, _786) {
    var v = MochiKit.Visual;
    var b = MochiKit.Base;
    _786 = b.update({ handle: false, starteffect: function(_789) {
        this._savedOpacity = MochiKit.Style.getStyle(_789, "opacity") || 1;
        new v.Opacity(_789, { duration: 0.2, from: this._savedOpacity, to: 0.7 });
    }, reverteffect: function(_78a, _78b, _78c) {
        var dur = Math.sqrt(Math.abs(_78b ^ 2) + Math.abs(_78c ^ 2)) * 0.02;
        return new v.Move(_78a, { x: -_78c, y: -_78b, duration: dur });
    }, endeffect: function(_78e) {
        new v.Opacity(_78e, { duration: 0.2, from: 0.7, to: this._savedOpacity });
    }, onchange: b.noop, zindex: 1000, revert: false, scroll: false, scrollSensitivity: 20, scrollSpeed: 15, snap: false
    }, _786);
    var d = MochiKit.DOM;
    this.element = d.getElement(_785);
    if (_786.handle && (typeof (_786.handle) == "string")) {
        this.handle = d.getFirstElementByTagAndClassName(null, _786.handle, this.element);
    }
    if (!this.handle) {
        this.handle = d.getElement(_786.handle);
    }
    if (!this.handle) {
        this.handle = this.element;
    }
    if (_786.scroll && !_786.scroll.scrollTo && !_786.scroll.outerHTML) {
        _786.scroll = d.getElement(_786.scroll);
        this._isScrollChild = MochiKit.DOM.isChildNode(this.element, _786.scroll);
    }
    MochiKit.Style.makePositioned(this.element);
    this.delta = this.currentDelta();
    this.options = _786;
    this.dragging = false;
    this.eventMouseDown = MochiKit.Signal.connect(this.handle, "onmousedown", this, this.initDrag);
    MochiKit.DragAndDrop.Draggables.register(this);
}, destroy: function() {
    MochiKit.Signal.disconnect(this.eventMouseDown);
    MochiKit.DragAndDrop.Draggables.unregister(this);
}, currentDelta: function() {
    var s = MochiKit.Style.getStyle;
    return [parseInt(s(this.element, "left") || "0"), parseInt(s(this.element, "top") || "0")];
}, initDrag: function(_791) {
    if (!_791.mouse().button.left) {
        return;
    }
    var src = _791.target();
    var _793 = (src.tagName || "").toUpperCase();
    if (_793 === "INPUT" || _793 === "SELECT" || _793 === "OPTION" || _793 === "BUTTON" || _793 === "TEXTAREA") {
        return;
    }
    if (this._revert) {
        this._revert.cancel();
        this._revert = null;
    }
    var _794 = _791.mouse();
    var pos = MochiKit.Position.cumulativeOffset(this.element);
    this.offset = [_794.page.x - pos.x, _794.page.y - pos.y];
    MochiKit.DragAndDrop.Draggables.activate(this);
    _791.stop();
}, startDrag: function(_796) {
    this.dragging = true;
    if (this.options.selectclass) {
        MochiKit.DOM.addElementClass(this.element, this.options.selectclass);
    }
    if (this.options.zindex) {
        this.originalZ = parseInt(MochiKit.Style.getStyle(this.element, "z-index") || "0");
        this.element.style.zIndex = this.options.zindex;
    }
    if (this.options.ghosting) {
        this._clone = this.element.cloneNode(true);
        this.ghostPosition = MochiKit.Position.absolutize(this.element);
        this.element.parentNode.insertBefore(this._clone, this.element);
    }
    if (this.options.scroll) {
        if (this.options.scroll == window) {
            var _797 = this._getWindowScroll(this.options.scroll);
            this.originalScrollLeft = _797.left;
            this.originalScrollTop = _797.top;
        } else {
            this.originalScrollLeft = this.options.scroll.scrollLeft;
            this.originalScrollTop = this.options.scroll.scrollTop;
        }
    }
    MochiKit.DragAndDrop.Droppables.prepare(this.element);
    MochiKit.DragAndDrop.Draggables.notify("start", this, _796);
    if (this.options.starteffect) {
        this.options.starteffect(this.element);
    }
}, updateDrag: function(_798, _799) {
    if (!this.dragging) {
        this.startDrag(_798);
    }
    MochiKit.Position.prepare();
    MochiKit.DragAndDrop.Droppables.show(_799, this.element);
    MochiKit.DragAndDrop.Draggables.notify("drag", this, _798);
    this.draw(_799);
    this.options.onchange(this);
    if (this.options.scroll) {
        this.stopScrolling();
        var p, q;
        if (this.options.scroll == window) {
            var s = this._getWindowScroll(this.options.scroll);
            p = new MochiKit.Style.Coordinates(s.left, s.top);
            q = new MochiKit.Style.Coordinates(s.left + s.width, s.top + s.height);
        } else {
            p = MochiKit.Position.page(this.options.scroll);
            p.x += this.options.scroll.scrollLeft;
            p.y += this.options.scroll.scrollTop;
            p.x += (window.pageXOffset || document.documentElement.scrollLeft || document.body.scrollLeft || 0);
            p.y += (window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0);
            q = new MochiKit.Style.Coordinates(p.x + this.options.scroll.offsetWidth, p.y + this.options.scroll.offsetHeight);
        }
        var _79d = [0, 0];
        if (_799.page.x > (q.x - this.options.scrollSensitivity)) {
            _79d[0] = _799.page.x - (q.x - this.options.scrollSensitivity);
        } else {
            if (_799.page.x < (p.x + this.options.scrollSensitivity)) {
                _79d[0] = _799.page.x - (p.x + this.options.scrollSensitivity);
            }
        }
        if (_799.page.y > (q.y - this.options.scrollSensitivity)) {
            _79d[1] = _799.page.y - (q.y - this.options.scrollSensitivity);
        } else {
            if (_799.page.y < (p.y + this.options.scrollSensitivity)) {
                _79d[1] = _799.page.y - (p.y + this.options.scrollSensitivity);
            }
        }
        this.startScrolling(_79d);
    }
    if (/AppleWebKit/.test(navigator.appVersion)) {
        window.scrollBy(0, 0);
    }
    _798.stop();
}, finishDrag: function(_79e, _79f) {
    var dr = MochiKit.DragAndDrop;
    this.dragging = false;
    if (this.options.selectclass) {
        MochiKit.DOM.removeElementClass(this.element, this.options.selectclass);
    }
    if (this.options.ghosting) {
        MochiKit.Position.relativize(this.element, this.ghostPosition);
        MochiKit.DOM.removeElement(this._clone);
        this._clone = null;
    }
    if (_79f) {
        dr.Droppables.fire(_79e, this.element);
    }
    dr.Draggables.notify("end", this, _79e);
    var _7a1 = this.options.revert;
    if (_7a1 && typeof (_7a1) == "function") {
        _7a1 = _7a1(this.element);
    }
    var d = this.currentDelta();
    if (_7a1 && this.options.reverteffect) {
        this._revert = this.options.reverteffect(this.element, d[1] - this.delta[1], d[0] - this.delta[0]);
    } else {
        this.delta = d;
    }
    if (this.options.zindex) {
        this.element.style.zIndex = this.originalZ;
    }
    if (this.options.endeffect) {
        this.options.endeffect(this.element);
    }
    dr.Draggables.deactivate();
    dr.Droppables.reset(this.element);
}, keyPress: function(_7a3) {
    if (_7a3.key().string != "KEY_ESCAPE") {
        return;
    }
    this.finishDrag(_7a3, false);
    _7a3.stop();
}, endDrag: function(_7a4) {
    if (!this.dragging) {
        return;
    }
    this.stopScrolling();
    this.finishDrag(_7a4, true);
    _7a4.stop();
}, draw: function(_7a5) {
    var pos = MochiKit.Position.cumulativeOffset(this.element);
    var d = this.currentDelta();
    pos.x -= d[0];
    pos.y -= d[1];
    if (this.options.scroll && (this.options.scroll != window && this._isScrollChild)) {
        pos.x -= this.options.scroll.scrollLeft - this.originalScrollLeft;
        pos.y -= this.options.scroll.scrollTop - this.originalScrollTop;
    }
    var p = [_7a5.page.x - pos.x - this.offset[0], _7a5.page.y - pos.y - this.offset[1]];
    if (this.options.snap) {
        if (typeof (this.options.snap) == "function") {
            p = this.options.snap(p[0], p[1]);
        } else {
            if (this.options.snap instanceof Array) {
                var i = -1;
                p = MochiKit.Base.map(MochiKit.Base.bind(function(v) {
                    i += 1;
                    return Math.round(v / this.options.snap[i]) * this.options.snap[i];
                }, this), p);
            } else {
                p = MochiKit.Base.map(MochiKit.Base.bind(function(v) {
                    return Math.round(v / this.options.snap) * this.options.snap;
                }, this), p);
            }
        }
    }
    var _7ac = this.element.style;
    if ((!this.options.constraint) || (this.options.constraint == "horizontal")) {
        _7ac.left = p[0] + "px";
    }
    if ((!this.options.constraint) || (this.options.constraint == "vertical")) {
        _7ac.top = p[1] + "px";
    }
    if (_7ac.visibility == "hidden") {
        _7ac.visibility = "";
    }
}, stopScrolling: function() {
    if (this.scrollInterval) {
        clearInterval(this.scrollInterval);
        this.scrollInterval = null;
        MochiKit.DragAndDrop.Draggables._lastScrollPointer = null;
    }
}, startScrolling: function(_7ad) {
    if (!_7ad[0] && !_7ad[1]) {
        return;
    }
    this.scrollSpeed = [_7ad[0] * this.options.scrollSpeed, _7ad[1] * this.options.scrollSpeed];
    this.lastScrolled = new Date();
    this.scrollInterval = setInterval(MochiKit.Base.bind(this.scroll, this), 10);
}, scroll: function() {
    var _7ae = new Date();
    var _7af = _7ae - this.lastScrolled;
    this.lastScrolled = _7ae;
    if (this.options.scroll == window) {
        var s = this._getWindowScroll(this.options.scroll);
        if (this.scrollSpeed[0] || this.scrollSpeed[1]) {
            var dm = _7af / 1000;
            this.options.scroll.scrollTo(s.left + dm * this.scrollSpeed[0], s.top + dm * this.scrollSpeed[1]);
        }
    } else {
        this.options.scroll.scrollLeft += this.scrollSpeed[0] * _7af / 1000;
        this.options.scroll.scrollTop += this.scrollSpeed[1] * _7af / 1000;
    }
    var d = MochiKit.DragAndDrop;
    MochiKit.Position.prepare();
    d.Droppables.show(d.Draggables._lastPointer, this.element);
    d.Draggables.notify("drag", this);
    if (this._isScrollChild) {
        d.Draggables._lastScrollPointer = d.Draggables._lastScrollPointer || d.Draggables._lastPointer;
        d.Draggables._lastScrollPointer.x += this.scrollSpeed[0] * _7af / 1000;
        d.Draggables._lastScrollPointer.y += this.scrollSpeed[1] * _7af / 1000;
        if (d.Draggables._lastScrollPointer.x < 0) {
            d.Draggables._lastScrollPointer.x = 0;
        }
        if (d.Draggables._lastScrollPointer.y < 0) {
            d.Draggables._lastScrollPointer.y = 0;
        }
        this.draw(d.Draggables._lastScrollPointer);
    }
    this.options.onchange(this);
}, _getWindowScroll: function(win) {
    var vp, w, h;
    MochiKit.DOM.withWindow(win, function() {
        vp = MochiKit.Style.getViewportPosition(win.document);
    });
    if (win.innerWidth) {
        w = win.innerWidth;
        h = win.innerHeight;
    } else {
        if (win.document.documentElement && win.document.documentElement.clientWidth) {
            w = win.document.documentElement.clientWidth;
            h = win.document.documentElement.clientHeight;
        } else {
            w = win.document.body.offsetWidth;
            h = win.document.body.offsetHeight;
        }
    }
    return { top: vp.y, left: vp.x, width: w, height: h };
}, repr: function() {
    return "[" + this.__class__.NAME + ", options:" + MochiKit.Base.repr(this.options) + "]";
} 
};
MochiKit.DragAndDrop.__new__ = function() {
    MochiKit.Base.nameFunctions(this);
    this.EXPORT_TAGS = { ":common": this.EXPORT, ":all": MochiKit.Base.concat(this.EXPORT, this.EXPORT_OK) };
};
MochiKit.DragAndDrop.__new__();
MochiKit.Base._exportSymbols(this, MochiKit.DragAndDrop);
MochiKit.Base._deps("Sortable", ["Base", "Iter", "DOM", "Position", "DragAndDrop"]);
MochiKit.Sortable.NAME = "MochiKit.Sortable";
MochiKit.Sortable.VERSION = "1.4.2";
MochiKit.Sortable.__repr__ = function() {
    return "[" + this.NAME + " " + this.VERSION + "]";
};
MochiKit.Sortable.toString = function() {
    return this.__repr__();
};
MochiKit.Sortable.EXPORT = [];
MochiKit.Sortable.EXPORT_OK = [];
MochiKit.Base.update(MochiKit.Sortable, { sortables: {}, _findRootElement: function(_7b7) {
    while (_7b7.tagName.toUpperCase() != "BODY") {
        if (_7b7.id && MochiKit.Sortable.sortables[_7b7.id]) {
            return _7b7;
        }
        _7b7 = _7b7.parentNode;
    }
}, _createElementId: function(_7b8) {
    if (_7b8.id == null || _7b8.id == "") {
        var d = MochiKit.DOM;
        var id;
        var _7bb = 1;
        while (d.getElement(id = "sortable" + _7bb) != null) {
            _7bb += 1;
        }
        d.setNodeAttribute(_7b8, "id", id);
    }
}, options: function(_7bc) {
    _7bc = MochiKit.Sortable._findRootElement(MochiKit.DOM.getElement(_7bc));
    if (!_7bc) {
        return;
    }
    return MochiKit.Sortable.sortables[_7bc.id];
}, destroy: function(_7bd) {
    var s = MochiKit.Sortable.options(_7bd);
    var b = MochiKit.Base;
    var d = MochiKit.DragAndDrop;
    if (s) {
        MochiKit.Signal.disconnect(s.startHandle);
        MochiKit.Signal.disconnect(s.endHandle);
        b.map(function(dr) {
            d.Droppables.remove(dr);
        }, s.droppables);
        b.map(function(dr) {
            dr.destroy();
        }, s.draggables);
        delete MochiKit.Sortable.sortables[s.element.id];
    }
}, create: function(_7c3, _7c4) {
    _7c3 = MochiKit.DOM.getElement(_7c3);
    var self = MochiKit.Sortable;
    self._createElementId(_7c3);
    _7c4 = MochiKit.Base.update({ element: _7c3, tag: "li", dropOnEmpty: false, tree: false, treeTag: "ul", overlap: "vertical", constraint: "vertical", containment: [_7c3], handle: false, only: false, hoverclass: null, ghosting: false, scroll: false, scrollSensitivity: 20, scrollSpeed: 15, format: /^[^_]*_(.*)$/, onChange: MochiKit.Base.noop, onUpdate: MochiKit.Base.noop, accept: null }, _7c4);
    self.destroy(_7c3);
    var _7c6 = { revert: true, ghosting: _7c4.ghosting, scroll: _7c4.scroll, scrollSensitivity: _7c4.scrollSensitivity, scrollSpeed: _7c4.scrollSpeed, constraint: _7c4.constraint, handle: _7c4.handle };
    if (_7c4.starteffect) {
        _7c6.starteffect = _7c4.starteffect;
    }
    if (_7c4.reverteffect) {
        _7c6.reverteffect = _7c4.reverteffect;
    } else {
        if (_7c4.ghosting) {
            _7c6.reverteffect = function(_7c7) {
                _7c7.style.top = 0;
                _7c7.style.left = 0;
            };
        }
    }
    if (_7c4.endeffect) {
        _7c6.endeffect = _7c4.endeffect;
    }
    if (_7c4.zindex) {
        _7c6.zindex = _7c4.zindex;
    }
    var _7c8 = { overlap: _7c4.overlap, containment: _7c4.containment, hoverclass: _7c4.hoverclass, onhover: self.onHover, tree: _7c4.tree, accept: _7c4.accept };
    var _7c9 = { onhover: self.onEmptyHover, overlap: _7c4.overlap, containment: _7c4.containment, hoverclass: _7c4.hoverclass, accept: _7c4.accept };
    MochiKit.DOM.removeEmptyTextNodes(_7c3);
    _7c4.draggables = [];
    _7c4.droppables = [];
    if (_7c4.dropOnEmpty || _7c4.tree) {
        new MochiKit.DragAndDrop.Droppable(_7c3, _7c9);
        _7c4.droppables.push(_7c3);
    }
    MochiKit.Base.map(function(e) {
        var _7cb = _7c4.handle ? MochiKit.DOM.getFirstElementByTagAndClassName(null, _7c4.handle, e) : e;
        _7c4.draggables.push(new MochiKit.DragAndDrop.Draggable(e, MochiKit.Base.update(_7c6, { handle: _7cb })));
        new MochiKit.DragAndDrop.Droppable(e, _7c8);
        if (_7c4.tree) {
            e.treeNode = _7c3;
        }
        _7c4.droppables.push(e);
    }, (self.findElements(_7c3, _7c4) || []));
    if (_7c4.tree) {
        MochiKit.Base.map(function(e) {
            new MochiKit.DragAndDrop.Droppable(e, _7c9);
            e.treeNode = _7c3;
            _7c4.droppables.push(e);
        }, (self.findTreeElements(_7c3, _7c4) || []));
    }
    self.sortables[_7c3.id] = _7c4;
    _7c4.lastValue = self.serialize(_7c3);
    _7c4.startHandle = MochiKit.Signal.connect(MochiKit.DragAndDrop.Draggables, "start", MochiKit.Base.partial(self.onStart, _7c3));
    _7c4.endHandle = MochiKit.Signal.connect(MochiKit.DragAndDrop.Draggables, "end", MochiKit.Base.partial(self.onEnd, _7c3));
}, onStart: function(_7cd, _7ce) {
    var self = MochiKit.Sortable;
    var _7d0 = self.options(_7cd);
    _7d0.lastValue = self.serialize(_7d0.element);
}, onEnd: function(_7d1, _7d2) {
    var self = MochiKit.Sortable;
    self.unmark();
    var _7d4 = self.options(_7d1);
    if (_7d4.lastValue != self.serialize(_7d4.element)) {
        _7d4.onUpdate(_7d4.element);
    }
}, findElements: function(_7d5, _7d6) {
    return MochiKit.Sortable.findChildren(_7d5, _7d6.only, _7d6.tree, _7d6.tag);
}, findTreeElements: function(_7d7, _7d8) {
    return MochiKit.Sortable.findChildren(_7d7, _7d8.only, _7d8.tree ? true : false, _7d8.treeTag);
}, findChildren: function(_7d9, only, _7db, _7dc) {
    if (!_7d9.hasChildNodes()) {
        return null;
    }
    _7dc = _7dc.toUpperCase();
    if (only) {
        only = MochiKit.Base.flattenArray([only]);
    }
    var _7dd = [];
    MochiKit.Base.map(function(e) {
        if (e.tagName && e.tagName.toUpperCase() == _7dc && (!only || MochiKit.Iter.some(only, function(c) {
            return MochiKit.DOM.hasElementClass(e, c);
        }))) {
            _7dd.push(e);
        }
        if (_7db) {
            var _7e0 = MochiKit.Sortable.findChildren(e, only, _7db, _7dc);
            if (_7e0 && _7e0.length > 0) {
                _7dd = _7dd.concat(_7e0);
            }
        }
    }, _7d9.childNodes);
    return _7dd;
}, onHover: function(_7e1, _7e2, _7e3) {
    if (MochiKit.DOM.isChildNode(_7e2, _7e1)) {
        return;
    }
    var self = MochiKit.Sortable;
    if (_7e3 > 0.33 && _7e3 < 0.66 && self.options(_7e2).tree) {
        return;
    } else {
        if (_7e3 > 0.5) {
            self.mark(_7e2, "before");
            if (_7e2.previousSibling != _7e1) {
                var _7e5 = _7e1.parentNode;
                _7e1.style.visibility = "hidden";
                _7e2.parentNode.insertBefore(_7e1, _7e2);
                if (_7e2.parentNode != _7e5) {
                    self.options(_7e5).onChange(_7e1);
                }
                self.options(_7e2.parentNode).onChange(_7e1);
            }
        } else {
            self.mark(_7e2, "after");
            var _7e6 = _7e2.nextSibling || null;
            if (_7e6 != _7e1) {
                var _7e5 = _7e1.parentNode;
                _7e1.style.visibility = "hidden";
                _7e2.parentNode.insertBefore(_7e1, _7e6);
                if (_7e2.parentNode != _7e5) {
                    self.options(_7e5).onChange(_7e1);
                }
                self.options(_7e2.parentNode).onChange(_7e1);
            }
        }
    }
}, _offsetSize: function(_7e7, type) {
    if (type == "vertical" || type == "height") {
        return _7e7.offsetHeight;
    } else {
        return _7e7.offsetWidth;
    }
}, onEmptyHover: function(_7e9, _7ea, _7eb) {
    var _7ec = _7e9.parentNode;
    var self = MochiKit.Sortable;
    var _7ee = self.options(_7ea);
    if (!MochiKit.DOM.isChildNode(_7ea, _7e9)) {
        var _7ef;
        var _7f0 = self.findElements(_7ea, { tag: _7ee.tag, only: _7ee.only });
        var _7f1 = null;
        if (_7f0) {
            var _7f2 = self._offsetSize(_7ea, _7ee.overlap) * (1 - _7eb);
            for (_7ef = 0; _7ef < _7f0.length; _7ef += 1) {
                if (_7f2 - self._offsetSize(_7f0[_7ef], _7ee.overlap) >= 0) {
                    _7f2 -= self._offsetSize(_7f0[_7ef], _7ee.overlap);
                } else {
                    if (_7f2 - (self._offsetSize(_7f0[_7ef], _7ee.overlap) / 2) >= 0) {
                        _7f1 = _7ef + 1 < _7f0.length ? _7f0[_7ef + 1] : null;
                        break;
                    } else {
                        _7f1 = _7f0[_7ef];
                        break;
                    }
                }
            }
        }
        _7ea.insertBefore(_7e9, _7f1);
        self.options(_7ec).onChange(_7e9);
        _7ee.onChange(_7e9);
    }
}, unmark: function() {
    var m = MochiKit.Sortable._marker;
    if (m) {
        MochiKit.Style.hideElement(m);
    }
}, mark: function(_7f4, _7f5) {
    var d = MochiKit.DOM;
    var self = MochiKit.Sortable;
    var _7f8 = self.options(_7f4.parentNode);
    if (_7f8 && !_7f8.ghosting) {
        return;
    }
    if (!self._marker) {
        self._marker = d.getElement("dropmarker") || document.createElement("DIV");
        MochiKit.Style.hideElement(self._marker);
        d.addElementClass(self._marker, "dropmarker");
        self._marker.style.position = "absolute";
        document.getElementsByTagName("body").item(0).appendChild(self._marker);
    }
    var _7f9 = MochiKit.Position.cumulativeOffset(_7f4);
    self._marker.style.left = _7f9.x + "px";
    self._marker.style.top = _7f9.y + "px";
    if (_7f5 == "after") {
        if (_7f8.overlap == "horizontal") {
            self._marker.style.left = (_7f9.x + _7f4.clientWidth) + "px";
        } else {
            self._marker.style.top = (_7f9.y + _7f4.clientHeight) + "px";
        }
    }
    MochiKit.Style.showElement(self._marker);
}, _tree: function(_7fa, _7fb, _7fc) {
    var self = MochiKit.Sortable;
    var _7fe = self.findElements(_7fa, _7fb) || [];
    for (var i = 0; i < _7fe.length; ++i) {
        var _800 = _7fe[i].id.match(_7fb.format);
        if (!_800) {
            continue;
        }
        var _801 = { id: encodeURIComponent(_800 ? _800[1] : null), element: _7fa, parent: _7fc, children: [], position: _7fc.children.length, container: self._findChildrenElement(_7fe[i], _7fb.treeTag.toUpperCase()) };
        if (_801.container) {
            self._tree(_801.container, _7fb, _801);
        }
        _7fc.children.push(_801);
    }
    return _7fc;
}, _findChildrenElement: function(_802, _803) {
    if (_802 && _802.hasChildNodes) {
        _803 = _803.toUpperCase();
        for (var i = 0; i < _802.childNodes.length; ++i) {
            if (_802.childNodes[i].tagName.toUpperCase() == _803) {
                return _802.childNodes[i];
            }
        }
    }
    return null;
}, tree: function(_805, _806) {
    _805 = MochiKit.DOM.getElement(_805);
    var _807 = MochiKit.Sortable.options(_805);
    _806 = MochiKit.Base.update({ tag: _807.tag, treeTag: _807.treeTag, only: _807.only, name: _805.id, format: _807.format }, _806 || {});
    var root = { id: null, parent: null, children: new Array, container: _805, position: 0 };
    return MochiKit.Sortable._tree(_805, _806, root);
}, setSequence: function(_809, _80a, _80b) {
    var self = MochiKit.Sortable;
    var b = MochiKit.Base;
    _809 = MochiKit.DOM.getElement(_809);
    _80b = b.update(self.options(_809), _80b || {});
    var _80e = {};
    b.map(function(n) {
        var m = n.id.match(_80b.format);
        if (m) {
            _80e[m[1]] = [n, n.parentNode];
        }
        n.parentNode.removeChild(n);
    }, self.findElements(_809, _80b));
    b.map(function(_811) {
        var n = _80e[_811];
        if (n) {
            n[1].appendChild(n[0]);
            delete _80e[_811];
        }
    }, _80a);
}, _constructIndex: function(node) {
    var _814 = "";
    do {
        if (node.id) {
            _814 = "[" + node.position + "]" + _814;
        }
    } while ((node = node.parent) != null);
    return _814;
}, sequence: function(_815, _816) {
    _815 = MochiKit.DOM.getElement(_815);
    var self = MochiKit.Sortable;
    var _816 = MochiKit.Base.update(self.options(_815), _816 || {});
    return MochiKit.Base.map(function(item) {
        return item.id.match(_816.format) ? item.id.match(_816.format)[1] : "";
    }, MochiKit.DOM.getElement(self.findElements(_815, _816) || []));
}, serialize: function(_819, _81a) {
    _819 = MochiKit.DOM.getElement(_819);
    var self = MochiKit.Sortable;
    _81a = MochiKit.Base.update(self.options(_819), _81a || {});
    var name = encodeURIComponent(_81a.name || _819.id);
    if (_81a.tree) {
        return MochiKit.Base.flattenArray(MochiKit.Base.map(function(item) {
            return [name + self._constructIndex(item) + "[id]=" + encodeURIComponent(item.id)].concat(item.children.map(arguments.callee));
        }, self.tree(_819, _81a).children)).join("&");
    } else {
        return MochiKit.Base.map(function(item) {
            return name + "[]=" + encodeURIComponent(item);
        }, self.sequence(_819, _81a)).join("&");
    }
} 
});
MochiKit.Sortable.Sortable = MochiKit.Sortable;
MochiKit.Sortable.__new__ = function() {
    MochiKit.Base.nameFunctions(this);
    this.EXPORT_TAGS = { ":common": this.EXPORT, ":all": MochiKit.Base.concat(this.EXPORT, this.EXPORT_OK) };
};
MochiKit.Sortable.__new__();
MochiKit.Base._exportSymbols(this, MochiKit.Sortable);
if (typeof (MochiKit) == "undefined") {
    MochiKit = {};
}
if (typeof (MochiKit.MochiKit) == "undefined") {
    MochiKit.MochiKit = {};
}
MochiKit.MochiKit.NAME = "MochiKit.MochiKit";
MochiKit.MochiKit.VERSION = "1.4.2";
MochiKit.MochiKit.__repr__ = function() {
    return "[" + this.NAME + " " + this.VERSION + "]";
};
MochiKit.MochiKit.toString = function() {
    return this.__repr__();
};
MochiKit.MochiKit.SUBMODULES = ["Base", "Iter", "Logging", "DateTime", "Format", "Async", "DOM", "Selector", "Style", "LoggingPane", "Color", "Signal", "Position", "Visual", "DragAndDrop", "Sortable"];
if (typeof (JSAN) != "undefined" || typeof (dojo) != "undefined") {
    if (typeof (dojo) != "undefined") {
        dojo.provide("MochiKit.MochiKit");
        (function(lst) {
            for (var i = 0; i < lst.length; i++) {
                dojo.require("MochiKit." + lst[i]);
            }
        })(MochiKit.MochiKit.SUBMODULES);
    }
    if (typeof (JSAN) != "undefined") {
        (function(lst) {
            for (var i = 0; i < lst.length; i++) {
                JSAN.use("MochiKit." + lst[i], []);
            }
        })(MochiKit.MochiKit.SUBMODULES);
    }
    (function() {
        var _823 = MochiKit.Base.extend;
        var self = MochiKit.MochiKit;
        var _825 = self.SUBMODULES;
        var _826 = [];
        var _827 = [];
        var _828 = {};
        var i, k, m, all;
        for (i = 0; i < _825.length; i++) {
            m = MochiKit[_825[i]];
            _823(_826, m.EXPORT);
            _823(_827, m.EXPORT_OK);
            for (k in m.EXPORT_TAGS) {
                _828[k] = _823(_828[k], m.EXPORT_TAGS[k]);
            }
            all = m.EXPORT_TAGS[":all"];
            if (!all) {
                all = _823(null, m.EXPORT, m.EXPORT_OK);
            }
            var j;
            for (j = 0; j < all.length; j++) {
                k = all[j];
                self[k] = m[k];
            }
        }
        self.EXPORT = _826;
        self.EXPORT_OK = _827;
        self.EXPORT_TAGS = _828;
    } ());
} else {
    if (typeof (MochiKit.__compat__) == "undefined") {
        MochiKit.__compat__ = true;
    }
    (function() {
        if (typeof (document) == "undefined") {
            return;
        }
        var _82e = document.getElementsByTagName("script");
        var _82f = "http://www.w3.org/1999/xhtml";
        var _830 = "http://www.w3.org/2000/svg";
        var _831 = "http://www.w3.org/1999/xlink";
        var _832 = "http://www.mozilla.org/keymaster/gatekeeper/there.is.only.xul";
        var base = null;
        var _834 = null;
        var _835 = {};
        var i;
        var src;
        for (i = 0; i < _82e.length; i++) {
            src = null;
            switch (_82e[i].namespaceURI) {
                case _830:
                    src = _82e[i].getAttributeNS(_831, "href");
                    break;
                default:
                    src = _82e[i].getAttribute("src");
                    break;
            }
            if (!src) {
                continue;
            }
            _835[src] = true;
            if (src.match(/MochiKit.js(\?.*)?$/)) {
                base = src.substring(0, src.lastIndexOf("MochiKit.js"));
                _834 = _82e[i];
            }
        }
        if (base === null) {
            return;
        }
        var _838 = MochiKit.MochiKit.SUBMODULES;
        for (var i = 0; i < _838.length; i++) {
            if (MochiKit[_838[i]]) {
                continue;
            }
            var uri = base + _838[i] + ".js";
            if (uri in _835) {
                continue;
            }
            if (_834.namespaceURI == _830 || _834.namespaceURI == _832) {
                var s = document.createElementNS(_834.namespaceURI, "script");
                s.setAttribute("id", "MochiKit_" + base + _838[i]);
                if (_834.namespaceURI == _830) {
                    s.setAttributeNS(_831, "href", uri);
                } else {
                    s.setAttribute("src", uri);
                }
                s.setAttribute("type", "application/x-javascript");
                _834.parentNode.appendChild(s);
            } else {
                document.write("<" + _834.nodeName + " src=\"" + uri + "\" type=\"text/javascript\"></script>");
            }
        }
    })();
}



import equals from 'fast-deep-equal';
import React, { useEffect, useRef } from 'react';
import { useEditor } from '@react-page/core/lib-es/Provider';
import { editable } from '@react-page/core/lib-es/selector/editable';
import HotKeyDecorator from '@react-page/core/lib-es/components/HotKey/Decorator';
import Inner from './Inner';
var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
var __rest = (this && this.__rest) || function (s, e) {
    var t = {};
    for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p) && e.indexOf(p) < 0)
        t[p] = s[p];
    if (s != null && typeof Object.getOwnPropertySymbols === "function")
        for (var i = 0, p = Object.getOwnPropertySymbols(s); i < p.length; i++) {
            if (e.indexOf(p[i]) < 0 && Object.prototype.propertyIsEnumerable.call(s, p[i]))
                t[p[i]] = s[p[i]];
        }
    return t;
};

var Editable = function (_a) {
    var id = _a.id, onChange = _a.onChange, rest = __rest(_a, ["id", "onChange"]);
    var editor = useEditor();
    var previousSerializedRef = useRef();
    useEffect(function () {
        var handleChanges = function () {
            var state = editable(editor.store.getState(), {
                id: id,
            });
            if (!state) {
                return;
            }
            // prevent uneeded updates
            var serialized = editor.plugins.serialize(state);
            var serializedEqual = equals(previousSerializedRef.current, serialized);
            if (serializedEqual) {
                return;
            }
            previousSerializedRef.current = serialized;
            onChange(serialized);
        };
        var unsubscribe = editor.store.subscribe(handleChanges);
        return function () {
            unsubscribe();
        };
    }, [editor, id, onChange]);
    return (React.createElement(HotKeyDecorator, { id: id },
        React.createElement(Inner, __assign({ id: id, defaultPlugin: editor.defaultPlugin }, rest))));
};
export default React.memo(Editable);
//# sourceMappingURL=index.js.map
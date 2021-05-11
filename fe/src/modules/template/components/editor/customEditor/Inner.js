import throttle from 'lodash.throttle';
import * as React from 'react';
import { createStructuredSelector } from 'reselect';
import { createFallbackCell } from '@react-page/core/lib-es/actions/cell';
import { connect } from '@react-page/core/lib-es/reduxConnect';
import { purifiedEditable } from '@react-page/core/lib-es/selector/editable';
import { ContentPlugin, LayoutPlugin } from '@react-page/core/lib-es/service/plugin/classes';
import Cell from '@react-page/core/lib-es/components/Cell';
import dimensions from '@react-page/core/lib-es/components/Dimensions';
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
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
/*
 * This file is part of ORY Editor.
 *
 * ORY Editor is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * ORY Editor is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with ORY Editor.  If not, see <http://www.gnu.org/licenses/>.
 *
 * @license LGPL-3.0
 * @copyright 2016-2018 Aeneas Rekkas
 * @author Aeneas Rekkas <aeneas+oss@aeneas.io>
 *
 */

function isElementInViewport(el) {
    var rect = el.getBoundingClientRect();
    return (rect.top >= 0 &&
        rect.left >= 0 &&
        rect.bottom <=
            (window.innerHeight ||
                document.documentElement.clientHeight) /*or $(window).height() */ &&
        rect.right <=
            (window.innerWidth ||
                document.documentElement.clientWidth) /*or $(window).width() */);
}
var Inner = /** @class */ (function (_super) {
    __extends(Inner, _super);
    function Inner() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.ref = React.createRef();
        _this.firstElementInViewport = null;
        _this.onScroll = throttle(function () {
            if (_this.ref.current) {
                var firstInViewport = Array.prototype.find.call(_this.ref.current.getElementsByClassName('ory-cell'), function (cell) { return isElementInViewport(cell); });
                if (firstInViewport) {
                    _this.firstElementInViewport = {
                        el: firstInViewport,
                        topOffset: firstInViewport.getBoundingClientRect().top,
                    };
                }
                else {
                    _this.firstElementInViewport = null;
                }
            }
        }, 600);
        _this.createFallbackCell = function () {
            var _a = _this.props, node = _a.node, defaultPlugin = _a.defaultPlugin, id = _a.id;
            if (!node) {
                return;
            }
            var _b = node.cells, cells = _b === void 0 ? [] : _b;
            if (cells.length === 0) {
                // FIXME: one more reason to unify layout and content plugins...
                if (defaultPlugin.createInitialChildren) {
                    _this.props.createFallbackCell(new LayoutPlugin(defaultPlugin), id);
                }
                else {
                    _this.props.createFallbackCell(new ContentPlugin(defaultPlugin), id);
                }
            }
        };
        return _this;
    }
    Inner.prototype.componentDidMount = function () {
        this.createFallbackCell();
        window.addEventListener('scroll', this.onScroll);
    };
    Inner.prototype.componentDidUpdate = function (oldProps) {
        this.createFallbackCell();
        // if (oldProps.displayMode !== this.props.displayMode) {
        //     if (this.firstElementInViewport) {
        //         var _a = this.firstElementInViewport, el_1 = _a.el, topOffset_1 = _a.topOffset;
        //         setTimeout(function () {
        //             scrollIntoViewWithOffset(el_1, topOffset_1, 'auto');
        //         }, 0);
        //     }
        // }
    };
    Inner.prototype.componentWillUnmount = function () {
        window.removeEventListener('scroll', this.onScroll);
    };
    Inner.prototype.render = function () {
        var _a = this.props, id = _a.id, containerWidth = _a.containerWidth, containerHeight = _a.containerHeight, node = _a.node, rest = __rest(_a, ["id", "containerWidth", "containerHeight", "node"]);
        if (!node) {
            return null;
        }
        var _b = node.cells, cells = _b === void 0 ? [] : _b;
        return (React.createElement("div", { ref: this.ref, className: "ory-editable" }, cells.map(function (c) { return (React.createElement(Cell, __assign({ rowWidth: containerWidth, rowHeight: containerHeight, editable: id, ancestors: [], key: c, id: c }, rest))); })));
    };
    return Inner;
}(React.PureComponent));
export var displayMode = function (_a) {
    var mode = _a.reactPage.display.mode;
    return mode;
};
var mapStateToProps = createStructuredSelector({
    node: purifiedEditable,
    displayMode: displayMode,
});
var mapDispatchToProps = { createFallbackCell: createFallbackCell };
export default dimensions()(connect(mapStateToProps, mapDispatchToProps)(Inner));
//# sourceMappingURL=index.js.map
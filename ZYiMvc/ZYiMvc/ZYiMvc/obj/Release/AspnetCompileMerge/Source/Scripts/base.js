/*HTML5 新元素对旧浏览器的支持*/
document.createElement("header");
document.createElement("section");
document.createElement("aside"); 
document.createElement("nav");

//Json对象转成Json格式字符串
var JSON; if (!JSON) { JSON = {} } (function () { 'use strict'; function f(n) { return n < 10 ? '0' + n : n }; if (typeof Date.prototype.toJSON !== 'function') { Date.prototype.toJSON = function (key) { return isFinite(this.valueOf()) ? this.getUTCFullYear() + '-' + f(this.getUTCMonth() + 1) + '-' + f(this.getUTCDate()) + 'T' + f(this.getUTCHours()) + ':' + f(this.getUTCMinutes()) + ':' + f(this.getUTCSeconds()) + 'Z' : null }; String.prototype.toJSON = Number.prototype.toJSON = Boolean.prototype.toJSON = function (key) { return this.valueOf() } }; var cx = /[\u0000\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g, escapable = /[\\\"\x00-\x1f\x7f-\x9f\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g, gap, indent, meta = { '\b': '\\b', '\t': '\\t', '\n': '\\n', '\f': '\\f', '\r': '\\r', '"': '\\"', '\\': '\\\\' }, rep; function quote(string) { escapable.lastIndex = 0; return escapable.test(string) ? '"' + string.replace(escapable, function (a) { var c = meta[a]; return typeof c === 'string' ? c : '\\u' + ('0000' + a.charCodeAt(0).toString(16)).slice(-4) }) + '"' : '"' + string + '"' }; function str(key, holder) { var i, k, v, length, mind = gap, partial, value = holder[key]; if (value && typeof value === 'object' && typeof value.toJSON === 'function') { value = value.toJSON(key) }; if (typeof rep === 'function') { value = rep.call(holder, key, value) }; switch (typeof value) { case 'string': return quote(value); case 'number': return isFinite(value) ? String(value) : 'null'; case 'boolean': case 'null': return String(value); case 'object': if (!value) { return 'null' }; gap += indent; partial = []; if (Object.prototype.toString.apply(value) === '[object Array]') { length = value.length; for (i = 0; i < length; i += 1) { partial[i] = str(i, value) || 'null' }; v = partial.length === 0 ? '[]' : gap ? '[\n' + gap + partial.join(',\n' + gap) + '\n' + mind + ']' : '[' + partial.join(',') + ']'; gap = mind; return v }; if (rep && typeof rep === 'object') { length = rep.length; for (i = 0; i < length; i += 1) { if (typeof rep[i] === 'string') { k = rep[i]; v = str(k, value); if (v) { partial.push(quote(k) + (gap ? ': ' : ':') + v) } } } } else { for (k in value) { if (Object.prototype.hasOwnProperty.call(value, k)) { v = str(k, value); if (v) { partial.push(quote(k) + (gap ? ': ' : ':') + v) } } } }; v = partial.length === 0 ? '{}' : gap ? '{\n' + gap + partial.join(',\n' + gap) + '\n' + mind + '}' : '{' + partial.join(',') + '}'; gap = mind; return v } }; if (typeof JSON.stringify !== 'function') { JSON.stringify = function (value, replacer, space) { var i; gap = ''; indent = ''; if (typeof space === 'number') { for (i = 0; i < space; i += 1) { indent += ' ' } } else if (typeof space === 'string') { indent = space }; rep = replacer; if (replacer && typeof replacer !== 'function' && (typeof replacer !== 'object' || typeof replacer.length !== 'number')) { throw new Error('JSON.stringify') }; return str('', { '': value }) } }; if (typeof JSON.parse !== 'function') { JSON.parse = function (text, reviver) { var j; function walk(holder, key) { var k, v, value = holder[key]; if (value && typeof value === 'object') { for (k in value) { if (Object.prototype.hasOwnProperty.call(value, k)) { v = walk(value, k); if (v !== undefined) { value[k] = v } else { delete value[k] } } } }; return reviver.call(holder, key, value) }; text = String(text); cx.lastIndex = 0; if (cx.test(text)) { text = text.replace(cx, function (a) { return '\\u' + ('0000' + a.charCodeAt(0).toString(16)).slice(-4) }) }; if (/^[\],:{}\s]*$/.test(text.replace(/\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g, '@').replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, ']').replace(/(?:^|:|,)(?:\s*\[)+/g, ''))) { j = eval('(' + text + ')'); return typeof reviver === 'function' ? walk({ '': j }, '') : j }; throw new SyntaxError('JSON.parse') } } } ());

//日期时间格式转换
Date.parse = function (s) { var t = s.replace('-', '/').replace('-', '/'); var d = new Date(t); if (d == NaN) d = new Date(s.replace('-', ',').replace('-', ',')); return d; }
Date.prototype.format = function (format) {
    var o = { "M+": this.getMonth() + 1, "d+": this.getDate(), "H+": this.getHours(), "m+": this.getMinutes(), "s+": this.getSeconds(), "q+": Math.floor((this.getMonth() + 3) / 3), "S": this.getMilliseconds() }
    if (/(y+)/.test(format)) format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length)); for (var k in o) if (new RegExp("(" + k + ")").test(format)) format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length)); return format;
}

//lambda表达式，Array返回List
Array.prototype.where = function (str) { var rs = []; for (var i in this) { var o = this[i]; if (typeof (this[i]) != 'function') if (eval(str)) rs.push(o); } return rs };


//随机数(100万内)
jQuery.nd = function () { return Math.round(Math.random(2) * 1000000) };
//允许拖拽
jQuery.fn.extend({ drayer: function (ops) { this.each(function () { var element = $(this); element.find('.zbox-header').mousedown(function (e) { if (!$(e.target).is('a,button,input')) { var biginX = e.clientX - element.position().left; var biginY = e.clientY - element.position().top; $('html').mousemove(function (e) { element.css({ top: e.clientY - biginY, left: e.clientX - biginX }) }).mouseup(function () { $('html').unbind('mousemove mouseup selectstart') }).bind("selectstart", function () { return false }) } }) }) } });
//模拟窗口(依赖拖拽+随机数) // 参数'close'或对象{title,width,height,create,beforeClose,buttons,top,left,draggable}
jQuery.fn.extend({ zbox: function (ops) { if (ops == 'close') { var zbox = $(this).hasClass('zbox') ? this[0] : $(this).parents('.zbox')[0]; $(zbox).data('ops').beforeClose(); $('div[byzbox=' + zbox.id + ']').remove(); $(zbox).remove(); return }; var s = this[0]; ops = jQuery.extend({ title: '提示', width: 300, height: 160, draggable: true, create: function () { }, beforeClose: function () { }, buttons: [{ text: '确定', click: function () { $(this).zbox('close') } }] }, ops); var nd = $.nd(); var overlay = '<div class="overlay" byzbox="zbox' + nd + '"></div>'; $(overlay).appendTo('body'); var html = '<div class="zbox" id="zbox' + nd + '"><div class="zbox-header"><span class="zbox-header-title">' + ops.title + '</span><a href="javascript:void(0);" class="zbox-close">×</a></div><div class="zbox-content">' + s.outerHTML + '</div><div class="zbox-toolbar"><div class="zbox-toolbar-btns"></div></div></div>'; $(html).appendTo('body'); var zbox = $('#zbox' + nd); $(zbox).data('ops', ops); ops.zbox = zbox; var buttons = zbox.find('.zbox-toolbar-btns'); $(ops.buttons).each(function (i, o) { buttons.append('<button>' + o.text + '</button>'); buttons.find('button').last().click({ zbox: zbox }, o.click) }); zbox.find('.zbox-close').click(function () { $(this).zbox('close') }); ops.draggable ? zbox.drayer() : 0; zbox.find('.zbox-content').css({ height: ops.height - 99 }); zbox.css({ width: ops.width + 4 }).css({ top: (ops.top || ($(window).height() - zbox.height()) / 2), left: (ops.left || ($(window).width() - zbox.width()) / 2) }); ops.create(); return zbox } });
//模拟窗口-alert||confirm
jQuery.jAlert = function (ops) { $('<div>' + (ops.msg || ops) + '</div>').zbox({ title: ops.title || '提示', btns: ops.btns || [{ text: '确定', click: function () { $(this).zbox('close') } }] }) }

//当前页码, 记录数, 总页数, 回调function
$.fn.pager = function (page, records, total, callback) {
    var html = '';
    if (total > 1) {
        html += '<a class="first" href="javascript:;">首页</a>';
        html += '<a class="first" href="javascript:;">上一页</a>';
        var start = page - 5 < 0 ? 0 : page - 5;
        var end = page + 5 < total ? page + 5 : total;
        for (var i = start; i < end; i++) {
            html += i == page - 1 ? '<span>' + (i + 1) + '</span>' : '<a href="javascript:;">' + (i + 1) + '</a>';
        }
        html += '<a class="first" href="javascript:;">下一页</a>';
        html += '<a class="last" href="javascript:;">末页</a>';
    }
    $(this).html(html);
    $(this).find('a').click(function () {
        var p = $(this).text();
        if (p == '上一页') p = page == 1 ? 1 : page - 1;
        if (p == '下一页') p = page == total ? total : page + 1;
        if (p == '首页') p = 1;
        if (p == '末页') p = total;

        if (p != page) callback(parseInt(p));
    });
}
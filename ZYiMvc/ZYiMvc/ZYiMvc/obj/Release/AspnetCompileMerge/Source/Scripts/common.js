var uploadFile = "http://v3.wwxww.cn/UploadFile/";
var defaultimage = "20140519200608967_goods.jpg";
//集中Ajax
function AJAX(url, prams, func, method, useLoading) {
    var _object = $(event.target);
    $.ajax({
        url: url,
        type: method || "POST",
        dataType: method ? 'html' : "JSON",
        contentType: method ? "text/html;charset=utf-8" : "application/json;charset=utf-8",
        data: JSON.stringify(prams),
        success: function (o, status, response) {
            if (o && o["Message"]) {
                ShowError('服务器错误');
                return;
            }
            if (o && o["loginTip"]) {
                open_login(_object);
                return;
            }
            if (o && o["loginRedirect"]) {
                window.location.href = "/Home/Index";
                return;
            }
            if (o && o['errorCode']) {
                if (o['param']) {
                    var text = T(o['errorCode'], o.d.param);
                    ShowError(text); //错误代码包含参数
                }
                else {
                    ShowError(o['errorCode']);
                }
                return;
            }
            func(o);//WebService返回带有d
        },
        error: function (e) {
            if (e.status == 0) return;

            if (e.status == 999) {//登陆超时
                ShowError('登陆超时');
                setTimeout('location.href="/login.html";', 2000);
            } else if (e.status == 998) {
                ShowError(e.responseText);
            }
            else {
                alert('请求状态: ' + e.status + '\n错误信息: ' + e.statusText);
            }
        },
        beforeSend: function (xhr) {
            if (useLoading != false)
                Loading();
        },
        complete: function (a, b, c) {
            if (useLoading != false)
                Loading('close');
        }
    });
}

function open_login(_object) {
    $(".login-modal,.login-overlay").addClass("login-show");
    $(document).keydown(function (e) {
        if (e.keyCode == 13) {
            $(".login-modal .login_btn").click();
        }
    });
    $(".login-modal .close_btn").click(function () {
        $(".login-modal,.login-overlay").removeClass("login-show");
    });
    $(".login-modal .login_btn").click(function () {
        var param = { No: $('#loginName').val(), Pwd: $('#loginPwd').val() };
        if (param.No.length == 0) {
            ShowError('请输入工号');
            return;
        }
        if (param.Pwd.length == 0) {
            ShowError('请输入密码');
            return;
        }
        AJAX('/Account/Login', param, function (d) {
            if (d.errorCode) {
                ShowError(d.errorCode);
            } else {
                var html = '<span class="sn-welcome-info"><span>Hi，</span><a target="_top" href="/Account/Index">' + d.No + '</a></span>';
                html += '<form action="/Account/LogOff" id="logoutForm" method="post"><span class="sn-welcome-info"><a class="sn-logout" target="_top" href="javascript:document.getElementById(\'logoutForm\').submit()" id="member_logout">退出</a></span></form>';
                $("#user_login_info").html(html);
                $(".login-modal,.login-overlay").removeClass("login-show");
                if (_object) {
                    _object.click();
                }
            }
        });
    });
    
}

//读取Html
function Load_Html(url, func) {
    AJAX(url, $.nd(), func, "GET");
}

//根据Code取权限
function getFunctionByCode(code, list) {
    var flag = undefined;
    list = list || _functionList;
    $(list).each(function () {
        if (this.code == code) {
            flag = this;
            return false;
        } else if (this.children) {
            $(this.children).each(function () {
                if (this.code == code) {
                    flag = this;
                } else if (this.children) {
                    $(this.children).each(function () {
                        if (this.code == code) {
                            flag = this;
                        }
                    });
                }
            });
        }
    });
    return flag
}

function Loading(cmd) {
    var i = $('body').data('loading_times') || 0;
    if (cmd != 'close') {
        if (i == 0) {
            $('<div class="overlay" byloading="true"></div><div class="loading" style="display:none"></div>').appendTo('body');
            $('.loading').show(0);
        }
        $('body').data('loading_times', ++i);
    } else {
        $('body').data('loading_times', --i);
    }
    if (i <= 0) {
        $('.loading').hide(0, function () { $('.overlay[byloading]').remove(); $(this).remove(); });
    }
}

//提示基础
function _Show(msg, type) {
    var show_func = $('body').data('show_func');
    var show_Timeout = $('body').data('show_Timeout');
    if (show_func && show_Timeout) {
        show_func();
        clearTimeout(show_Timeout);
    }

    var div = $('<div class="' + type + '"><strong>' + msg + '</strong></div>').appendTo('body');
    show_func = function () { div.remove() };
    show_Timeout = setTimeout(show_func, 3600);
    $('body').data({ show_func: show_func, show_Timeout: show_Timeout });
}


function ShowTip(msg) {
    if ($('.tipMsg').length > 0) {
        clearTimeout($('body').data('show_Timeout'));
        var show_Timeout = setTimeout($('body').data('show_func'), 4500);
        $('body').data({ show_Timeout: show_Timeout });
        $('.tipMsg strong').html(msg);
        return;
    }
    _Show(msg, 'tipMsg fadeIn animated');
}
function ShowError(msg) {
    if ($('.errorMsg').length > 0) {
        clearTimeout($('body').data('show_Timeout'));
        var show_Timeout = setTimeout($('body').data('show_func'), 4500);
        $('body').data({ show_Timeout: show_Timeout });
        $('.errorMsg strong').html(msg);
        return;
    }
    _Show(msg, 'errorMsg shake animated');
}

//获取右框架宽度，用于设置
function maxFrameWidth() {
    return $('body').width() - 223;
}

//确认提示
function Confirm(msg, okBtn) {
    $('<div>' + msg + '</div>').dialog({
        title: '提示',
        width: 310,
        height: 180,
        resizable: false,
        modal: true,
        buttons: [{
            'text': '确定',
            'click': okBtn
        }, {
            'text': '取消',
            'click': function () { $(this).dialog('close'); }
        }]
    });
}

//清除空白,参数一:父元素开始查找,排除参数二
function TrimAll(T, unTrim) {
    $(T).find('input').not(unTrim).each(function () {
        $(this).val($.trim($(this).val()));
    });
}

//是否整数
function isInteger(value) {
    if (value == undefined || value == null || value == "")
        return false;
    return value ? (value.toString().match(/^\d+$/) ? true : false) : false;
}
//是否邮箱
function isEmail(value) {
    var p = /^[-!#$%&\'*+\\./0-9=?A-Z^_`a-z{|}~]+@[-!#$%&\'*+\\/0-9=?A-Z^_`a-z{|}~]+\.[-!#$%&\'*+\\./0-9=?A-Z^_`a-z{|}~]+$/;
    return p.test(value);
}

//是否手机号码
function isMobile(value) {
    var p = /(^1[3|4|5|8]\d{9}$)|(^\++\d{2,}$)/;
    return p.test(value);
}
//准备增删改查权限-清除操作
function ClearNoRight() {
    var list = _currentPageCode.children;
    !list.where('o.code.indexOf("view")!=-1')[0] ? $(cFrame).find('.view').hide().parent().removeAttr('title') : '';
    !list.where('o.code.indexOf("edit")!=-1')[0] ? $(cFrame).find('.edit').hide().parent().removeAttr('title') : '';
    !list.where('o.code.indexOf("add")!=-1')[0] ? $(cFrame).find('.add').hide() : '';
    !list.where('o.code.indexOf("delete")!=-1')[0] ? $(cFrame).find('.delete').hide().parent().removeAttr('title') : '';
    !list.where('o.code.indexOf("active")!=-1')[0] ? $(cFrame).find('.active').hide().parent().removeAttr('title') : '';
    !list.where('o.code.indexOf("import")!=-1')[0] ? $(cFrame).find('.import').hide().parent().removeAttr('title') : '';
    !list.where('o.code.indexOf("export")!=-1')[0] ? $(cFrame).find('.export').hide().parent().removeAttr('title') : '';
}

//填充下拉框    - 数组对象,  目标元素, 第一个选项文本(或false)  , id ,文本, 
function FillSelect(d, target, defaultItem, vId, vText) {
    vId = vId ? vId : 'ID';
    vText = vText ? vText : 'Name';
    var options = defaultItem ? '<option value="' + defaultItem[vId] + '">' + defaultItem[vText] + '</option>' : '';
    $(d).each(function (i, o) {
        options += '<option value="' + o[vId] + '">' + o[vText] + '</option>';
    });
    $(target).html(options);
}


//计算arr里面物质数量
function CountMatter(arr, key) {
    var count = 0;
    $(arr).each(function (i, o) {
        count += o[key].length;
    });
    return count;
}


//创建上传工具
function CreateUploader(url, browseBtn, uploadBtn, complete, auto) {
    _uploadObj.complete = complete;
    var id = 'f' + new Date().getTime();
    $('<iframe id="' + id + '" frameborder="no"></iframe>').css({ 'display': 'none' }).appendTo($(browseBtn).parent());
    var ifString = '<html><head><meta http-equiv="Content-Type" content="text/html; charset=utf-8" /></head><body><form method="post" id="form1" action="' + url + '" enctype="multipart/form-data"><input id="file" type="file" name="file" /></form></body></html>';
    (window.frames[id].document || window.frames[$('iframe').length - 1].document).writeln(ifString); // FF需要用序号，不支持id
    var doc = _uploadObj.doc = $(window.frames[id].document || window.frames[$('iframe').length - 1].document);
    doc.find('#file').change(function (e) {
        var arr = e.target.value.split('.');
        if (_uploadObj.extendNames.where('o=="' + arr[arr.length - 1] + '"').length > 0) {
            $(browseBtn).parent().find('input[type=text]').val(e.target.value);
            if (auto) {
                _uploadObj.submit();
            }
        } else {
            $([$(browseBtn).prev()[0], e.target]).val('');
            ShowError("文件格式仅支持" + _uploadObj.extendNames.join(','));
        }
    });
    $(browseBtn).unbind('click').click(function () {
        doc.find('#file').click();
    });
    $(uploadBtn).unbind('click').click(function () {
        if (doc.find('#file').first().val() == '')
            ShowError("请先选择文件");
        else
            _uploadObj.submit();
    });

    return {
        reset: function () {
            $('#' + id).remove();
            $(browseBtn).parent().find('input[type=text]').val('');
            CreateUploader(url, browseBtn, uploadBtn, complete, auto);
        }
    };
}

var _uploadObj = {
    extendNames: ['xls', 'xlsx', 'zip', 'rar', 'jpg', 'gif'],
    doc: null,
    submit: function () {
        //loading
        Loading();
        this.doc.find('form').submit();
    }, complete: function (code) {
        //un loading

    }, download: function (url) {
        var id = 'f' + new Date().getTime();
        var ifString = '<div class="hide"><iframe id="' + id + '" name="' + id + '" frameborder="no" src="' + url + '"></iframe></div>';
        $('body').append(ifString);
    }
};

//获取文件后缀
function checkimage(file_name) {
    var result = /.[^.]+$/.exec(file_name);;
    if (result != ".jpg" && result != ".png" && result != ".gif" && result != ".bmp") {
        return false;
    } else {
        return true;
    }
}

function getNowString() {
    var _dt = new Date();
    return [_dt.getFullYear(), _dt.getMonth(), _dt.getDate()].join('-') + ' ' + [_dt.getHours(), _dt.getMinutes(), _dt.getSeconds()].join(':');
}


//验证价格,两位小数
function isPrice(_keyword) {
    if (_keyword == "0" || _keyword == "0." || _keyword == "0.0" || _keyword == "0.00") {
        _keyword = "0"; return true;
    } else {
        var index = _keyword.indexOf("0");
        var length = _keyword.length;
        if (index == 0 && length > 1) {/*0开头的数字串*/
            var reg = /^[0]{1}[.]{1}[0-9]{1,2}$/;
            if (!reg.test(_keyword)) {
                return false;
            } else {
                return true;
            }
        } else {/*非0开头的数字*/
            var reg = /^[1-9]{1}[0-9]{0,10}[.]{0,1}[0-9]{0,2}$/;
            if (!reg.test(_keyword)) {
                return false;
            } else {
                return true;
            }
        }
        return false;
    }
}
//获取当前时间
function CurentTime() {
    var now = new Date();

    var year = now.getFullYear();       //年
    var month = now.getMonth() + 1;     //月
    var day = now.getDate();            //日

    var hh = now.getHours();            //时
    var mm = now.getMinutes();          //分

    var clock = year + "-";

    if (month < 10)
        clock += "0";

    clock += month + "-";

    if (day < 10)
        clock += "0";

    clock += day + " ";

    if (hh < 10)
        clock += "0";

    clock += hh + ":";
    if (mm < 10) clock += '0';
    clock += mm;
    return (clock);
}


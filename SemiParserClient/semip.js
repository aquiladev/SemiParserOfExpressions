$(document).ready(function () {
    var expressionInput = $("#expTxt");
    var expressionBlock = $('#expBlock');
    var requestBlock = $('#pBlock');
    var replaceBlock = $('#replaceBlock');

    expressionInput.focus();
    $("body").keypress(function (event) {
        if (event.which == 13) {
            if (expressionBlock.is(':visible')) {
                goParseAction(event);
            }
            else if (requestBlock.is(':visible')) {
                goReplaceAction(event);
            }
            else if (replaceBlock.is(':visible')) {
                goToStart(event);
            }
        }
    });

    $("#pBlock input").live("change", function () {
        var current = $(this);
        $("#pBlock input").each(function () {
            if ($(this).attr('id') != current.attr('id')
                && $(this).attr('uid') == current.attr('uid')) {
                $(this).val(current.val());
            }
        });
    });


    function goParseAction(event) {
        event.stopPropagation();
        var exp = expressionInput.val();
        $.ajax({
            type: "get",
            contentType: "application/json; charset=utf-8",
            url: "SemiService.svc/Verify",
            dataType: "json",
            data: {
                expression: exp
            },
            success: function (data) {
                if (data) {
                    $.ajax({
                        type: "get",
                        contentType: "application/json; charset=utf-8",
                        url: "SemiService.svc/Parse",
                        dataType: "json",
                        data: {
                            expression: exp
                        },
                        success: function (data) {
                            createEditForm($.map(data, function (item) {
                                return {
                                    offset: item.Offset,
                                    length: item.Length,
                                    name: item.Name
                                }
                            }));
                        }
                    })
                }
                else {
                    alert("Что-то с выражением не то!");
                }
            }
        });

        function createEditForm(variables) {
            var exp = expressionInput.val().replace(/\s+/g, "");
            show();
            var table = $("#tblRequest");
            var prevLastIndex = 0;
            var index = 0;
            for (index in variables) {
                var entry = variables[index];
                if (entry.offset != prevLastIndex) {
                    var value = exp.substring(prevLastIndex, entry.offset);
                    addLabelCell(value);
                }
                addTextCell(entry.name, index);
                prevLastIndex = entry.offset + entry.length;
                index = index + 1;
            }
            if (prevLastIndex < exp.length) {
                var value = exp.substring(prevLastIndex, exp.length);
                addLabelCell(value);
            }
            table.find("tr:eq(1) td input").eq(0).focus();

            function show() {
                expressionBlock.hide();
                replaceBlock.hide();
                requestBlock.show();
            }

            function addLabelCell(value) {
                table.find("tr:eq(0)").append("<td></td>");
                table.find("tr:eq(1)").append("<td>" + value + "</td>");
            }

            function addTextCell(value, index) {
                table.find("tr:eq(0)").append("<td>" + value + "</td>");
                table.find("tr:eq(1)").append("<td><input type='text' id='blaInp" + index + "' uId='" + value + "'></input></td>");
            }
        }
    }

    function goReplaceAction(event) {
        event.stopPropagation();
        show();
        var table = $("#tblRequest");
        var td = table.find("tr:eq(1) td");
        var inputs = td.find("input");
        var variables = $.map(inputs, function (elt, i) { return $(elt).val(); });
        validVariables(variables);

        function show() {
            expressionBlock.hide();
            requestBlock.hide();
            replaceBlock.show();
        }

        function validVariables(variables) {
            $.ajax({
                type: "post",
                contentType: "application/json; charset=utf-8",
                url: "SemiService.svc/VerifyVariables",
                dataType: "json",
                data: JSON.stringify({ variables: variables }),
                success: function (data) {
                    if (data.VerifyVariablesResult) {
                        buildExpression($("#tblRequest tr:eq(1) td"))
                    }
                    else {
                        alert('Переменные могут быть только целочисленными!');
                        expressionBlock.hide();
                        $("#replaceBlock").hide();
                        requestBlock.show();
                        $("#tblRequest tr:eq(1) td input").eq(0).focus();
                    }
                }
            });
        }

        function buildExpression(td) {
            var expRes = "";
            $.each(td, function (index) {
                expRes = (td[index].children.length > 0) ? expRes + td[index].children[0].value : expRes + td[index].innerHTML;
            });
            $("#repLabel").text(expRes);
        }
    }

    function goToStart(event) {
        event.stopPropagation();
        expressionInput.val("");
        show();
        expressionInput.focus();
        $("#tblRequest td").remove();

        function show() {
            expressionBlock.show();
            replaceBlock.hide();
            requestBlock.hide();
        }
    }
});
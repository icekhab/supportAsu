function LoadGrid(url) {
    $("#dictionary").jqGrid({
        url: url,
        colNames: ['', 'name', 'code'],
        datatype: "json",
        mtype: 'POST',
        colModel: [
                { name: 'id', index: 'id', align: 'center', hidden: true },
                { name: 'Name', index: 'Name', align: 'center', resizable: false },
                 { name: 'Code', index: 'Code', align: 'center', resizable: false }
        ],
        pager: $('#dictionary_pager_grid'),
        rowList: [5, 10, 20],
        width: 700,
        sortname: "Id",
        sortorder: 'asc',
        rowNum: 5,
        loadonce: true,
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records"
        },
        height: '100%',
        onSelectRow: function (id) {
            $('#dic_value').css("display", "");
            var itemId = $("#dictionary").jqGrid('getCell', id, 'id');
            $("#dictionary_value").setGridParam({ datatype: 'json', page: 1, postData: { itemId: itemId } }).trigger('reloadGrid');
        }
    });
}
function LoadValueGrid(url) {
    $("#dictionary_value").jqGrid({
        url: url,
        colNames: ['', '','', 'name', 'code', ''],
        datatype: "json",
        mtype: 'POST',
        colModel: [
            { name: 'edit', index: 'edit', align: 'center', width: '20', sortable: false, resizable: false },
                { name: 'id', index: 'id', align: 'center', hidden: true },
                 { name: 'dicId', index: 'dicId', align: 'center', hidden: true },
                { name: 'value', index: 'value', align: 'center', width: '330', resizable: false },
                 { name: 'code', index: 'code', align: 'center', width: '330', resizable: false },
                 { name: 'del', index: 'del', align: 'center', width: '20', sortable: false, resizable: false }
        ],
        pager: $('#dictionary_value_pager_grid'),
        rowList: [5, 10, 20],
        width: 700,
        sortname: "Id",
        sortorder: 'asc',
        rowNum: 5,
        //loadonce: true,
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records"
        },
        autowidth: true,
        height: '100%',
        gridComplete: function () {
            var ids = $("#dictionary_value").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var cl = ids[i];
                var row = $("#dictionary_value").jqGrid('getRowData', cl);
                btnEdit = "<input type='button' class='btn btn-edit' onclick=\"AddEdit(" + row["dicId"] + "," + row["id"] + ")\"  />";
                btnDelete = "<input type='button' class='btn btn-delete' onclick=Delete(" + row["id"] +","+row["dicId"]+ ") />";

                jQuery("#dictionary_value").jqGrid('setRowData', ids[i], { edit: btnEdit });
                jQuery("#dictionary_value").jqGrid('setRowData', ids[i], { del: btnDelete });
            }
        }
    });
}

function AddEdit(dicId, itemId) {
    $.ajax({
        url: addEditUrl,
        cache: false,
        data:
            {
                dicId: dicId,
                itemId: itemId
            },
        type: "GET",
        async: true,
        success: function (data) {
            var div = document.createElement('div');
            div.id = "DV_dialog";
            $(div).appendTo('body').html(data).dialog({
                width: 600,
                height: 'auto',
                resizable: false,
                modal: true,
                show: { effect: "drop", direction: "right" },
                hide: { effect: "drop", direction: "left" },
                title: itemId == null ? 'Додання' : 'Редагування',
                closeOnEscape: true,
                zIndex: 900,
                close: function () {
                    $(this).dialog('destroy').html("").remove();
                }
            });
        }
    });
}

function Delete(itemId,dicId) {
    MessageDialogYesNo('Видалити запис?', function () {
        $.ajax({
            url: delUrl,
            cache: false,
            data:
                {
                    dicId: dicId,
                    itemId: itemId
                },
            async: false,
            type: 'POST',
            success: function (data) {
                $("#dictionary_value").setGridParam({ datatype: 'json', postData: { itemId: dicId } }).trigger('reloadGrid');
            }
        });
    }, null)

}
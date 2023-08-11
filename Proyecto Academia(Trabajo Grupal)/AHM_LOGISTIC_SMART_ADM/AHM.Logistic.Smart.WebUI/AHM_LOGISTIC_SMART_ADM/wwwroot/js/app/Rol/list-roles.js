Roles = (function () {
    var obj = {};
    obj.configureRole = function () {
        $(function ($) {
            var $componentsTree = $("#components-tree"),
                $itemsInput = $("#ModuleItemsInput");
            $componentsTree.jstree({
                plugins: ["checkbox", "search"],
            }).on("ready.jstree", function () {
                if ($itemsInput.val()) {
                    var selected = $itemsInput.val().split(",");
                    for (var i = 0; i < selected.length; i++) {
                        $componentsTree.jstree(true).select_node("item-" + selected[i]);
                    }
                }
            }).on("changed.jstree", function () {
                var selected = $componentsTree.jstree("get_selected", true)
                    .map(function (x) { return x.data.id; })
                    .filter(function (y) { return y !== undefined; });
                $itemsInput.val(selected);
            });
        });
    }
    return obj;
}());

function Nuevo() {
    window.location = '/Rol/Edit';
}


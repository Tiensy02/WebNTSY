<template>
        <div class="table-main">
            <m-table @changePageSize="changeValuePaging" @changePageCurrent="changeValuePaging" @clickUpdate="clickUpdate"
                @addRecord="addEmulationTitle" @removeRecord="removeEmulationTitle" @updateCheckAll="updateCheckAll"
                @handlerClickMenuMain="handlerClickMenuMain" :columns="columns" :records="emulationTitles"
                :recordSelecteds="emulationTitleSlecteds" :moreActionTables="moreActions" itemID="EmulationID"
                nameItemActiveID="EmulationStatus">
            </m-table>
    </div>
</template>

<script>
export default {
    name: "emulation-titles",

    computed: {

        // đối tượng chứa thông tin tìm kiếm
        emulationTiTleFilterInfo() {
            return this.$store.state.emulationTiTleFilterInfo
        },
        //trạng thái của bộ lọc
        isFilter() {
            return this.$store.state.isFilter
        },
        // trạng thái ô input
        isSearch() {
            return this.$store.state.isSearch
        },
        // số bản ghi trên 1 trang
        pageSize() {
            return this.$store.state.pageSize
        },
        // trang hiện tại
        pageCurrent() {
            return this.$store.state.pageCurrent
        }


    },
    data() {
        return {
            // tên, chiều rộng, và dữ liệu cần truyền cho cột đó
            columns: [{ name: "Tên danh hiệu thi đua", widthColumn: "column-310", columnCode: 'EmulationName' },
            { name: "Mã danh hiệu", widthColumn: "column-160", columnCode: 'EmulationCode' },
            { name: "Đối tượng khen thưởng", widthColumn: "column-180", columnCode: 'EmulationAwardRecipient' },
            { name: "Cấp khen thưởng", widthColumn: "column-200", columnCode: 'AwardName' },
            { name: "Loại phong trào", widthColumn: "column-200", columnCode: 'EmulationMovementType' },
            { name: "Trạng thái", widthColumn: "column-180", columnCode: 'EmulationStatus' }],
            // menu những tiện ích trong table
            moreActions: [
                {
                    id: this.$_MEnum.EmulationStatus.Active, name: this.$_MResource['VN'].EmulationStatus.Active, info: ""
                },
                {
                    id: this.$_MEnum.EmulationStatus.NoneActive, name: this.$_MResource['VN'].EmulationStatus.NoneActive, info: ""
                },
                {
                    id: this.$_MEnum.Action.Delete, name: this.$_MResource['VN'].Active.Delete, info: "danger"
                }
            ]
        }
    },
    watch: {

    },

    methods: {
        /**
         * @description gọi đến hàm update ở component Main
         * @param {Object} record đối tượng được chọn để chỉnh sửa
         */
        clickUpdate(record) {
            this.$emit("updateEmulationTitle", record)
        },
        /**
         * thực hiện khi thông tin về trang thi đổi
         */
        changeValuePaging() {
            if (this.isFilter || this.isSearch) {
                this.$store.dispatch("getEmulationTitleFilterAsync", {
                    page: this.pageCurrent,
                    pageSize: this.pageSize,
                    filterObject: this.EmulationTiTleFilterInfo
                })
            } else {
                this.$store.dispatch("getEmulationTitleAsync", {
                    pageCurrent: this.pageCurrent,
                    pageSize: this.pageSize
                });
            }
        },
        /**
         * @description nếu chưa tồn tại trong danh sách thì thêm bản ghi được chọn vào danh sách
         * @param {Object} record đối tượng bản ghi nhận được
         */
        addEmulationTitle(record) {
            this.$store.commit("addEmulationTitleSelecteds", record);
        },
        /**
         * nếu bản ghi đã có trong danh sách thì xóa đi
         * @param {*} record dối tượng bản ghi được chọn
         */
        removeEmulationTitle(record) {
            this.$store.commit("removeEmulationTitleSelecteds", record)
        },
        /**
         * nếu checkbox control checked thì sẽ chọn tất cả
         * nếu checkbox control uncheckes thì sẽ xóa tất cả
         * @param {Boolean} value giá trị của ô checkbox control
         */
        updateCheckAll(value) {
            if (value) {
                this.$store.commit("addAllEmulationTitle")
            } else {
                if (this.emulationTitleSlecteds.length == this.emulationTitles.length) {
                    this.$store.commit("removeAllEmulationTitleSelecteds")
                }
            }
        },
        /**
         * @description thực hiện xử lí khi click vào action trong menu của table
         * nếu là sự kiện xóa thì gọi hàm xóa ở compo nent cha và gửi đối tượng đang được thực thi
         * nếu là sự kiện sửa thì gọi hàm sửa ở component cha và gửi cho status cần sửa cùng với ID của đối tượng đang được chọn
         * @param{Number} idMenu id của item được chọn trong menu actions của table
         * @param{Object} record đối tượng bản ghi  
         */
        handlerClickMenuMain(idMenu, record) {

            if (idMenu == this.$_MEnum.Action.Delete) {
                this.$emit("deleteEmulation",record);
            } else {
                this.$emit("updateEmulationStatus",idMenu,record.EmulationID)
            }
        }
    },
    props: {
        emulationTitleSlecteds: {
            type: Array
        },
        emulationTitles: {
            type: Array,
            default: () => []
        },
        loading:{
            type:Boolean
        } 
    }
}
</script>

<style></style>
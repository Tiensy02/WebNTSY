<template>
    <div class="table-box">
        <div class="table-wrap" ref="tableWrap">
            <table ref="tabel">
                <thead>
                    <tr>
                        <th v-if="hasCheckColumn" class="column-56 stiky-left-column">
                            <m-checkbox :valueCheckbox="recordSelecteds.length == records.length"
                                :hasCheckboxTrue="recordSelecteds.length > 0 && recordSelecteds.length < records.length"
                                @updateValueCheckbox="changeCheckboxMaster"></m-checkbox>
                        </th>
                        <th v-for="(column, index) in columns" :class="column.widthColumn" :key="index">
                            <div class="th-title"> {{ column.name }}</div>
                        </th>
                    </tr>
                </thead>
                <tbody v-if="loadingData">
                    <div v-for="(record, indexOfRecords) in records" style="display: table-row;">
                        <div v-if="hasCheckColumn" class="column-56" style="height: 30px; display: table-cell;">
                            <div class="skeleton"></div>
                        </div>
                        <div v-for="(column, indexOfColumns) in columns" style="height: 30px; display: table-cell"
                            :key="indexOfColumns" :class="column.widthColumn">
                            <div class="skeleton"></div>
                        </div>
                    </div>
                </tbody>
                <tbody v-else>
                    <tr v-for="(record, indexOfRecords) in records" :key="indexOfRecords" class="row-table"
                        @dblclick="onClickUpdate(record)" @mouseleave="isShowMenuAction = false">
                        <td v-if="hasCheckColumn" class="column-56 stiky-left-column" >
                            <m-checkbox :valueCheckbox="isRecordSelected(record)"
                                @updateValueCheckbox="changeCheckbox($event, record)"></m-checkbox>
                        </td>
                        <td v-for="(column, indexOfColumns) in columns " :key="indexOfColumns" :class="column.widthColumn">
                            <div class="td-content">{{ converString(column.columnCode, record[column.columnCode]) }}</div>
                        </td>
                        <div class="more-action" ref="moreAction">
                            <div class="action-item" @click="onClickUpdate(record)">
                                <div class="icon icon-fix tooltip--top"> <span class="tooltiptext">Sửa</span></div>
                            </div>
                            <div class="action-item" @click="isShowMenuAction = true">
                                <div class="icon icon-triggle tooltip--top">
                                    <span class="tooltiptext">Thêm</span>
                                </div>
                                <m-context-menu :items="moreActionTables" v-if="isShowMenuAction"
                                    :activeID="record[nameItemActiveID]"
                                    @handlerClickMenu="handlerClickMenu($event, record)"></m-context-menu>
                            </div>
                        </div>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="table__paging">
            <span>Tổng số: <b>{{ totalRecord }}</b> bản ghi</span>
            <div class="paging-control flex">
                <p class="paging-title">Số bản ghi/trang</p>
                <div class="select-record flex">
                    <m-dropdown-list :menuSelect="recordsPaging" :defaultValue="10"
                        @updateVauleDropdownList="updateVauleDropdownList"></m-dropdown-list>
                </div>
                <div class="paging">
                    <p><b>{{ recordStart }} - {{ recordEnd }} </b>bản ghi</p>
                    <div class="paging-toogle">
                        <div ref="prevPage" class="icon icon-prev" :class="{ 'no-active': !isPrevPageAbel }"
                            @click="clickPrePage"></div>
                        <div ref="afterPage" class="icon icon-after" :class="{ 'no-active': !isNextPageAbel }"
                            @click="clickAfterPage"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import converString from "../helper/conver-string.js";
export default {
    name: "m-table",
    mounted() {
        this.checkAfterPageAbel()
        this.checkPrevPageAbel()
    },

    data() {
        return {
            // danh sách các pageSize 
            recordsPaging: [{ id: "1", name: "10" },
            { id: "2", name: "20" },
            { id: "3", name: "50" },
            { id: "4", name: "100" }],
            isNextPageAbel: true,
            isPrevPageAbel: false,
            isShowMenuAction: false,
            loading: false,
        }
    },
    watch: {
        // nếu bảng table thay đổi thì cập nhập dấu phân trang
        records(){
            this.checkAfterPageAbel()
            this.checkPrevPageAbel()
        },
        /**
         * @description khi pageSize thay đổi sẽ gọi cập nhập pageSize và gọi hàm thay đổi pageSize ở component cha
         */
        pageSize(newpageSize) {
            this.checkAfterPageAbel()
            this.checkPrevPageAbel()
            this.$store.commit("updatePageSize", newpageSize)
            this.$emit("changePageSize")
        },
        /**
         * thay đổi trang hiện tại
         * @param {Number} newPageCurrent trang hiện tại
         */
        pageCurrent(newPageCurrent) {
            this.$emit("changePageCurrent")
        },

    }, 
    computed: {

        loadingData() {
            return this.$store.state.loadingData
        },
        /**
         * @description computed tính toán index của bản ghi đầu tiên trên trang hiện tại
         * @return index của bản ghi đầu tiên trên trang hiện tại
         */
        recordStart() {
            if (this.pageCurrent == 1) return this.pageCurrent
            else {
                return (this.pageCurrent - 1) * this.pageSize + 1;
            }
        },
        /**
         * @description computed tính toán index của bản ghi cuối  trên trang hiện tại
         * @return index của bản ghi cuối trên trang hiện tại
         */
        recordEnd() {
            if (this.records.length == this.pageSize) {
                return this.pageCurrent * this.records.length
            } else {
                return (this.pageCurrent - 1) * this.pageSize + this.records.length
            }
        },
        // tổng số trang
        totalRecord() {
            return this.$store.state.totalRecord
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
    methods: {
        /**
         * @description kiểm tra xem bản ghi có trong danh sách bản ghi theo id được chọn hay chưa,
         * nếu có trả về true ngược lại trả về false
         * @param {Object} record bản ghi cần kiểm tra
         */
        isRecordSelected(record) {
            if (this.recordSelecteds.find(element => element[this.itemID] == record[this.itemID]) === undefined) {
                return false;
            } else {
                return true;
            }
        },
        // khi click vào checkbox ở header thì sẽ chọn hoặc bỏ tất cả cá checkbox ở dưới
        changeCheckboxMaster(value) {
            this.$emit("updateCheckAll", value);
        },
        /**
         * khi có 1 check box bị thay đổi sẽ cập nhập record, nếu check box là true thì thêm 
         * vào danh sách record được chọn nếu check box false thì xóa đi
         * @param {Boolean} dataCheckbox : check box được chọn hay không
         * @param {Object} record : đối tượng được gửi đi
         */
        changeCheckbox(dataCheckbox, record) {
            if (dataCheckbox) {
                this.$emit("addRecord", record)
            } else {
                this.$emit("removeRecord", record)
            }
        },
        /**
         * khi click vào item update thì sẽ truyền cho component cha đối tượng của bản ghi để xử lí
         * @param {*} record 
         */
        onClickUpdate(record) {
            this.$emit("clickUpdate", record)
        },
        /**
       * @description chuyển đổi giá trị enum sang chuỗi mô tả
       * @param {*} filed trường cần được chỉnh sửa 
       * @param {*} value giá trị nhận được
       * @return hàm chuyển đổi
       */
        converString(filed, value) {
            return converString(filed, value);
        },
        /**
         * cập nhập giá trị từ component MDropdownList khi có sự thay đổi và gán cho pageSize
         * @param {*} value giá trị của MDropdownList gửi cho
         */
        updateVauleDropdownList(value) {
            this.$store.commit("updatePageSize", value)
        },
        /**
         * @description click vào icon prev sẽ trở về trang trước
         */
        clickPrePage() {
            this.checkPrevPageAbel();
            if (this.isPrevPageAbel) {
                this.$store.commit("updatePageCurrent", this.pageCurrent - 1)
                this.isNextPageAbel = true
                this.checkPrevPageAbel()
            }

        },
        // click vào icon after page thì sẽ đến trang tiếp theo
        clickAfterPage() {
            this.checkAfterPageAbel()
            if (this.isNextPageAbel) {
                this.$store.commit("updatePageCurrent", this.pageCurrent + 1)
                this.isPrevPageAbel = true
                if ((this.pageCurrent) * this.pageSize >= this.totalRecord) {
                    this.isNextPageAbel = false
                }
            }

        },
        /**
         * @description hàm thực hiện kiểm tra có được phép cộng trang tiếp không
         */
        checkAfterPageAbel() {

            if (this.recordEnd >= this.totalRecord) {
                this.isNextPageAbel = false;
            } else {
                this.isNextPageAbel = true
            }
        }, /**
         * @description hàm thực hiện kiểm tra có được phép lùi trang tiếp không
         */
        checkPrevPageAbel() {
            if (this.pageCurrent == 1) {
                this.isPrevPageAbel = false
            } else {
                this.isPrevPageAbel = true
            }
        },
        /**
         * xử lí sự kiện click vào tiện ích trong menu
         * @param {Number} idMenu id của hành động được chọn từ Menu
         * @param {Object} record bản ghi
         */
        handlerClickMenu(idMenu, record) {
            this.$emit("handlerClickMenuMain", idMenu, record)
            this.isShowMenuAction = false
        }


    },

    props: {
        /**
         *  mảng chứa các object chỉ định các cột sẽ có dạng sau, các cột của 
         * {name: "tên cột",
         *  widthColumn: chiều rộng của cột có dạng "column-chiều dài mong muốn"} ví dụ: widthColumn: "column-300"
         *  columnCode: tên để lấy dữ liệu của bản ghi ví dụ: EmulationCode
         */
        columns: {
            type: Array,
            default: []
        },
        // có cột check hay không
        hasCheckColumn: {
            type: Boolean,
            default: true
        },
        // số lượng bản ghi
        records: {
            type: Array,
            default: [],
        },

        // danh sách bản ghi được chọn
        recordSelecteds: {
            type: Array
        },
        // tên của trường lưu trữ id của bản các bản ghi
        itemID: {
            type: String,
            default: "id"
        },
        // danh sách menu của các tiện ích của table
        moreActionTables: {
            type: Array,
            default: () => {
                return []
            }
        },
        // tên trường lưu trữ id của item menu được active
        nameItemActiveID: {
            type: String
        },

    }
}
</script>

<style>
@import url('../styles/base/sekeleton.css');
@import url('../styles/base/table.css');
</style>
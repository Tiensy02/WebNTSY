<template>
    <div class="combobox-wrap">
        <label v-if="isLabel" for="" :class="[{ request: isRequest }, 'form-label']">{{ labelText }}</label>
        <div class="combobox" ref="comboboxData" :class="{ 'is-valid': hasError, 'combobox-disabled': disabled }">
            <input ref="comboboxInput" type="text" class="input" @focus="onFocus" v-model="valueSelect"
                :class="{ 'input-disabled': disabled }" @keydown.down="showDataByKeyDown" @keydown.up="handlerKeyUp"
                @keyup.enter="handlerEnterItem">
            <div class="icon-combo" @click="ShowDrowByIcon"></div>
        </div>
        <span v-if="hasError" class="text-error-mess">{{ errorMess }}</span>
        <!-- Bảng data tương ứng với dữ liệu trong ô input-->
        <div class="combobox__drop-wrap" v-if="isShowDrow">
            <div v-for="item in itemFilters" :key="item[itemID]" ref="itemData"
                :class="['combobox-item', { 'item-active': item[itemName] == valueSelect }]"
                @click="onSelected(item[itemName])">
                <div class="item-title">{{ item[itemName] }}</div>
                <div v-if="isIcon & item[itemName] == valueSelect" class="icon icon-tick"></div>
            </div>
        </div>
        <!-- Bảng data hiện thị tất cả item khi ấn vào icon-->
        <div class="combobox__drop-wrap" v-if="isShowDrowByIcon">
            <div v-for="item in items" :key="item[itemID]" ref="itemData"
                :class="['combobox-item', { 'item-active': item[itemName] == valueSelect }]"
                @click="onSelected(item[itemName])">
                <div class="item-title">{{ item[itemName] }}</div>
                <div v-if="isIcon & item[itemName] == valueSelect" class="icon icon-tick"></div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: "m-combobox",
    mounted() {
        // thực hiện gửi ô input của combobox cho component cha xử lí
        this.$emit("sendInputComboboxElement", this.$refs.comboboxInput)
        // cập nhập giá trị ban đầu của combobox cho các component cha
        this.getValueDefault(this.defaultValue)
        let valueObject = {
            name: this.$refs.comboboxInput,  //thành phần input
            newValue: this.defaultValue
        }
        this.$emit('updateValueCombobox', valueObject)
    },
    data() {
        return {
            isShowDrow: false,
            isShowDrowByIcon: false,
            valueSelect: "",
            hasError: false,
            errorMess: '',
            isEmptyCombobox: this.isEmpty
        }
    },
    watch: {
        /**
         * @description: kiểm tra giá trị của input để đưa goi các hàm giải quyết validate
         * @param {*} value giá trị của ô input
         * @author ntsy(10/07/2023)
         * 
         */
        valueSelect(value) {
            try {
                this.isShowDrowByIcon = false
                /** nếu trường nhập không được phép để trống sẽ kiểm tra giá trị value và gọi hàm xử lí validate trống, truyền cho component cha 1 object gồm ô input bị lỗi và mess lỗi */
                if (this.isEmptyCombobox == false) {
                    if (value.trim() == "") {
                        this.hasError = true;
                        this.isShowDrow = false;
                        this.errorMess = `${this.labelText} ${this.$_MResource['VN'].InvalidError.Empty}`
                        let errorObject = {
                            name: this.$refs.comboboxInput, // thành phần input
                            mess: this.errorMess
                        }
                        this.$emit("onValidateEmpty", errorObject)
                    } else this.hasError == false
                }
                /**kiểm tra giá trị trong ô input có phù hợp với các item trong danh sách data của combobox không */
                if (this.items.filter((item) => {
                    return item[this.itemName].toLowerCase().includes(value.toLowerCase()); // trả về 1 danh sách các item chứa kí tự trong ô input
                }).length == 0) {
                    this.hasError = true;
                    this.isShowDrow = false;
                    this.errorMess = this.$_MResource['VN'].InvalidError.NotExist(this.labelText)
                    let errorObject = {
                        name: this.$refs.comboboxInput, // thành phần input
                        mess: this.errorMess
                    }
                    this.$emit('onValidateExist', errorObject)
                } else this.hasError = false
                // khi không có lỗi => gọi hàm cập nhập giá trị và truyền cho component cha 1 object gồm ô input đó và giá trị
                if (this.hasError == false) {
                    let valueObject = {
                        name: this.$refs.comboboxInput, // thành phần input
                        newValue: this.getIDbyName(value)
                    }
                    this.isShowDrow = true
                    // nếu giá trị của input thoả mãn thì đóng bảng data
                    this.items.forEach(item => {
                        if (item[this.itemName] == value) this.isShowDrow = false
                    });
                    this.$emit('updateValueCombobox', valueObject)
                }
            } catch (e) {
                console.log("error in MCombobox.vue ~ " + e)
            }
        },
        // xử lí dữ liệu ban đầu
        computedItems(items) {
            try {
                for (let i = 0; i < items.length; i++) {
                    if (this.defaultValue == items[i][this.itemID]) {
                        this.valueSelect = items[i][this.itemName]
                    }
                }
            } catch (e) {
                console.log("lỗi gán dữ liệu ban đầu ở combobox ~ " + e.message);
            }
        },
    },

    computed: {
        /**
         * @description computed trả về danh sách item hợp lệ với với input nhập vào
         */
        itemFilters() {
            if (!this.items) {
                return []
            }
            return this.items.filter((item) => {
                return item[this.itemName].toLowerCase().includes(this.valueSelect.toLowerCase());
            })
        },

        // xử lí khi nào items được gán xong
        computedItems() {
            return this.items
        },
    },

    methods: {
        /**
         * @description trả về id theo tên
         * @param {*} name tên của đối tượng
         * @author ntsy(21/07/2023)
         */
        getIDbyName(name) {
            for (let i = 0; i < this.items.length; i++) {
                if (name == this.items[i][this.itemName]) return this.items[i][this.itemID]
            }
            return null;
        },
        /**
         * @description trả về tên theo id
         * @param {*} id id của đối tượng
         */
        getValueDefault(id) {
            try {
                for (let i = 0; i < this.items.length; i++) {
                    if (id == this.items[i][this.itemID]) {
                        this.valueSelect = this.items[i][this.itemName]
                    }
                }
            } catch (e) {
                console.log("lỗi gán dữ liệu ban đầu ~ " + e.message)
            }
        },

        /**
         * @description: mở danh sách khi focus vào input
         */
        onFocus() {
            if(this.$refs.comboboxInput.value.trim() != "") {
                this.$refs.comboboxInput.select()
            }
            if (this.itemFilters.length != 0) this.isShowDrow = true
            this.items.forEach(item => {
                if (item[this.itemName] == this.valueSelect) this.isShowDrow = false
            });
            this.isShowDrowByIcon = false
            window.addEventListener('click', this.clickOutSideCombobox)
        },
        /**
         * @description thực hiện chức năng chọn items
         * @param {*} item : item được chọn
         */
        onSelected(newValue) {
            this.valueSelect = newValue;
            this.isShowDrowByIcon = false
            this.isShowDrow = false
        },
        /**
         * @description: xử lí sự kiện click ra ngoài combobox
         * @param {*} e event của sự kiện click
         */
        clickOutSideCombobox(e) {
            try {
                if (this.$refs.comboboxData) {
                    if (!this.$refs.comboboxData.contains(e.target)) {
                        this.closeComboboxData()
                    }
                }
            } catch (err) {
                console.log("error of 'closeComboboxData' in MCombobox ~ " + err)
            }
        },
        closeComboboxData() {
            this.isShowDrow = false;
            this.isShowDrowByIcon = false;
            window.removeEventListener('click', this.clickOutSideCombobox)
        },
        /**
         * @description các thao tác ẩn hiện bảng data
         * @author ntsy(10/07/2023)
         */
        ShowDrowByIcon() {
            if (this.isShowDrow) {
                this.isShowDrow = false
            } else this.isShowDrowByIcon = !this.isShowDrowByIcon
            window.addEventListener('click', this.clickOutSideCombobox)
        },
        // thực hiện mở bảng data khi ấn nút mũi tên xuống cà chọn các item
        showDataByKeyDown() {
            if (!this.isShowDrow) {
                this.isShowDrowByIcon = true
            }
            if (this.isShowDrow || this.isShowDrowByIcon) {

                let hasItemHover = false
                var elements = this.$refs.itemData
                if (elements) {
                    if (elements.length != 0) {
                        for (let i = 0; i < elements.length; i++) {
                            if (elements[i].classList.contains("item-hover")) {
                                hasItemHover = true
                                if (i == elements.length - 1) {
                                    elements[elements.length - 1].classList.remove("item-hover")
                                    elements[0].classList.add("item-hover")
                                }
                                else {
                                    elements[i].classList.remove("item-hover")
                                    elements[i + 1].classList.add("item-hover")
                                }
                                break
                            }
                        }
                        if (!hasItemHover) {
                            elements[0].classList.add("item-hover")
                        }
                    }
                }
            }
        },
        // thực hiện hover các item khi ấn nút mũi tên lên
        handlerKeyUp() {
            if (this.isShowDrow || this.isShowDrowByIcon) {
                let hasItemHover = false
                var elements = this.$refs.itemData
                // nếu element tồn tại thực hiện kiểm tra tiếp element có phần tử hay không, nếu có tiếp tục thực hiện
                if (elements) {
                    if (elements.length != 0) {
                        for (let i = 0; i < elements.length; i++) {
                            if (elements[i].classList.contains("item-hover")) {
                                hasItemHover = true
                                if (i == 0) {
                                    elements[0].classList.remove("item-hover")
                                    elements[elements.length - 1].classList.add("item-hover")
                                }
                                else {
                                    elements[i].classList.remove("item-hover")
                                    elements[i - 1].classList.add("item-hover")
                                }
                                break
                            }
                        }
                        if (!hasItemHover) {
                            elements[elements.length - 1].classList.add("item-hover")
                        }
                    }
                }
            }
        },
        // thực hiện chọn item khi ấn enter
        handlerEnterItem(event) {
            try {
                if (this.isShowDrow || this.isShowDrowByIcon) {
                    this.$refs.itemData.forEach(ele => {
                        if (ele.classList.contains("item-hover")) {
                            this.onSelected(ele.innerText)
                        }
                    })
                }
            } catch (err) {
                console.error(er.message)
            }
        }
    },
    props: {
        /** trường có bắt buộc nhập hay không */
        isRequest: {
            type: Boolean,
            defaule: false
        },
        /** có nhãn hay không */
        isLabel: {
            type: Boolean,
            defaule: false
        },
        /** tên label */
        labelText: {
            type: String,
            defaule: "label"
        },
        /**danh sách data trong combobox */
        items: {
            type: Array,
            required: true
        },
        /**sau khi chọn item thì item có icon đi kém không */
        isIcon: {
            type: Boolean,
            default: false
        },

        /** trường có được phép để trống không */
        isEmpty: {
            required: true

        },
        /** tên của trường lưu trữ id của items */
        itemID: {
            type: String,
            default: "id"
        },
        /** tên của trường lưu trữ tên của items */

        itemName: {
            type: String,
            default: "name"
        },
        // giá trị ban đầu là đối tượng có ít nhất name và id
        defaultValue:
        {
            required: true,
        },
        // combobox có phải disable không
        disabled: {
            type: Boolean,
            default: false
        }

    },

}
</script>

<style>
@import url(../styles/base/combobox.css);
</style>
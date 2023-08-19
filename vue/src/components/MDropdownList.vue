<template>
    <div class="dropdown-list-wrap">
        <div class="dropdown-list-box" ref="dropdownList">
            <input type="text" class="input" readonly  v-model="selectedValue">
            <div class="icon-combo" @click="onclickDown"></div>
        </div>
        <div v-if="isShowDrow"  ref="dropdownListData" class="dropdown-list-data" >
            <div class="items" :class="{'item-active': item.name == selectedValue}" 
            v-for="(item,index) in menuSelect" :key="index" @click="onSelected(item)">
                <div class="item-title">{{ item.name }}</div>
                <div v-if="item.name == selectedValue" class="icon icon-tick"></div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: "m-dropdown-list",
    data() {
        return {
            isShowDrow :false,
            selectedValue: this.defaultValue
        }
    },
    watch:{
        selectedValue(value){
            this.$emit("updateVauleDropdownList", value)
        }
    },
    methods:{
        /**
         * thực hiện chọn item, sau khi chọn được thì tắt bảng data
         * @param {*} item item được chọn
         */
        onSelected(item) {
            this.selectedValue = item.name;
            this.isShowDrow = false;
        },

        /**
         * thực thiện tắt mở bảng data khi click vào icon dropdown
         */

        onclickDown(){
            this.isShowDrow = ! this.isShowDrow;
            window.addEventListener('click',this.clickOutSideDropdownList)
        },
        /**
         * thực hiện đóng bảng data khi click ra ngoài bảng
         */
        clickOutSideDropdownList(e){
            try{
                if(this.$refs.dropdownList){
                    if(!this.$refs.dropdownList.contains(e.target)) {
                        this.closeDropdownList()
                    }
                    
                }
            }catch(err) {
                console.log("error of 'closeDropdownList' in MCombobox ~ "+ err)
            }
        },
        closeDropdownList(){
            this.isShowDrow = false;
            window.removeEventListener('click',this.clickOutSideDropdownList)
        },
    },
    props:{
        // danh sách item để chọn, danh sách bắt buộc phải có trường name
        menuSelect: {
            required: true
        },
        // vị trí của bảng data: có 2 giá trị: "bottom" và "top"
        positionDropdownData:{
            type: String,
        },
        // Dữ liệu ban đầu của dropdow
        defaultValue:{
            
        }
    }
}
</script>

<style>
@import url('../styles/base/dropdown-list.css');
</style>
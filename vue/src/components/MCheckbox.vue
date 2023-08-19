<template>
    <div class="checkbox-container" @mouseover="handlerMouseOver()"
             @mouseout="handlerMouseOut()">
        <label class="checkbox-wrap" ref="checkboxWrap">
            <input  type="checkbox" class="check-box" v-model="isChecked" >
            <span class="checkmak"
             :class="{ 'check-true': isChecked&&!hasCheckboxTrue ,'check-master':hasCheckboxTrue}"></span>
        </label> 
        <span v-if="islabel" class="check-name" ref="checkLabel">{{ labelName }}</span>
    </div>
</template>
  
<script>
export default {
    props: {
        // check box có label hay không
        islabel:{
            type: Boolean,
            default: false
        },
        // tên của label nếu có
        labelName:{
            type: String,
            default: "labelName"
        },
        // giá trị ban đầu của checkbox
        valueCheckbox:{
            type: Boolean
        },
        
        hasCheckboxTrue:{
            type:Boolean,
            default:false
        }

    },
    watch:{
        // theo dõi sự thay đổi của giá trị value ở component cha
        valueCheckbox(newValue) {
            this.isChecked = newValue;
        },
        isChecked(newValue) {
            this.$emit("updateValueCheckbox", newValue);
        }
    },
    data() {
        return {
            isChecked: this.valueCheckbox,
        };
    },
    methods: {
        /**
         * @description hàm thực hiện thêm class hover vào cho checkbox-wrap khi di chuột vào phần tử chứa ô check
         * box và label của nó
         */
         handlerMouseOver(){
            this.$refs.checkboxWrap.classList.add("checkbox-wrap-hover")
        },
         /**
         * @description hàm thực hiện xóa class hover  cho checkbox-wrap khi di chuột vào phần tử chứa ô check
         * box và label của nó
         */
        handlerMouseOut(){
            this.$refs.checkboxWrap.classList.remove("checkbox-wrap-hover")
        },
    },
};
</script>
  
<style>
@import url('../styles/base/checkbox.css');
</style>
   
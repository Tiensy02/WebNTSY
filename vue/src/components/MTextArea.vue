<template>
    <div class="text-area-wrap">
        <label v-if="isLabel" for="" class="form-label">{{ labelText }}</label>
        <textarea ref="textArea" name="" class="form-textarea" maxlength="255" :placeholder="(disabled) ? '' : $_MResource['VN'].Active.InputNote" rows="4"
            v-model="valueTextAre" :disabled="disabled">
        </textarea>
    </div>
</template>

<script>
export default {
    name: "m-text-area",
    mounted(){
        // thực hiện gửi dữ liệu ban đầu cho component cha
        let dataInput = {
                name:this.$refs.textArea,
                newValue:this.valueText
            }
        this.$emit("updateValue",dataInput)
        // thực hiện gửi textarea element cho component cha xử lí
        this.$emit("sendTextAreaElement",this.$refs.textArea)
    },
    data() {
        return {
            valueTextAre:this.valueText
        }
    },
    watch:{
        /**
         * @description thực hiện cập nhập dữ liệu khi có thay đổi
         * @param {String} newValue giá trị của text area
         */
        valueTextAre(newValue){
            let dataInput = {
                name:this.$refs.textArea,
                newValue:newValue
            }
            this.$emit("updateValue",dataInput)
        }
    },
    props: {

        isLabel:{
            type:Boolean,
            default:true
        },
        labelText:{
            type:String
        },
        disabled:{
            type:Boolean,
            default:false
        },
        valueText:{
            type:String,
            default:""
        }
    }
}
</script>

<style>
@import url('../styles/base/text-area.css');
</style>
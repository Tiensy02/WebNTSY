<template>
    <div>
        <label class="request form-label">{{ labelText }}</label>
        <div class="radio-item-wrap">
            <div class="radio-item" v-for="(item,index) in items" :key="index">
                <input class="radio-input" ref="radioInput" type="radio"  :name="nameRadio" :value="item[itemID]" 
                v-model="valueSelected">
                <span class="content-item" @click="valueSelected=item[itemID]"> {{ item[itemName] }}</span>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name:"m-radio-input",
    mounted(){
        // thực hiện gửi các input element cho component cha xử lí
        this.$refs.radioInput.forEach(ele=>{
            this.$emit("sendRadioInputElement", ele)
        })
    },
    data() {
        return {
            valueSelected:this.defaultValue
        }
    },
    watch:{
        /**
         * @description thực hiện cập nhập giá trị cho component cha khi có thay đổi ở radio input
         * @param {Number} value giá trị của radio
         */
        valueSelected(value) {
            let valueObject = {
                    name: this.labelText,
                    newValue: value,
                }
            this.$emit("updateRadioInput",valueObject)
        }
    },
    props:{
        // label của radioinput
        labelText:{
            type:String,
            default:"label"
        },
        // danh sách lựa chọn của radio input
        items:{
            type:Array,
            default:()=>{
                return []
            }
        },
        // tên của trường lưu trữ id của 1 item trong danh sách
        itemID:{
            type:String,
            default:"id"
        },
        // tên của trường lưu trữ tên hiện thị
        itemName:{
            typeof:String,
            default:"name"
        },
        // tên của radio input 
        nameRadio:{
            type:String,
            required:true
        },
        // giá trị ban đầu là id của item được tick ban đầu
        defaultValue:{
            type:Number
        }
    }
}
</script>

<style>
@import url('../styles/base/radio-input.css');
</style>
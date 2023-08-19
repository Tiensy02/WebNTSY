<template>
    <div class="context-menu-wrap">
        <div class="context-menu-item" v-for="(item, index) in items " :key="index"
        :class="{'context-menu-item-active': isActive(item[itemID]), 'item-danger':(item[itemInfo]=='danger')}"
        @click=" ( !isActive(item[itemID]) )? handlerClickMenu(item[itemID]) :null "
        >{{ item[itemName]  }}</div>
    </div>
</template>

<script>
export default{
    name:'m-context-menu',
    data(){
        return {

        }
    },
    methods:{
        /**
         * hàm kiểm tra xem item có phải là item được đã được chọn không
         * @param {Number} id id của item trong danh sách
         */
        isActive(id){
            if(id == this.activeID){
                return true;
            }else return false;
        },
        /**
         * hàm xử lí sự kiện click vào 1 item trong menu, gọi hàm thực hiện ở component cha
         * @param {Number} id id của item được chọn
         */
        handlerClickMenu(id){
            this.$emit("handlerClickMenu",id)
        }
    },
    props:{
        // danh sách menu, mỗi item có 3 trường id, name, info
        items:{
            type:Array,
            default:()=> {
                return []
            }
        },
        // tên trường lưu trữ giá trị id của id
        itemID:{
            type:String,
            default:"id"
        },
        // tên trường lưu giá trị tên hiện thị 
        itemName:{
            type:String,
            default:"name"
        },
        // tên trường lưu giá trị info của item, dangerr, warm
        itemInfo:{
            type:String,
            default:"info"
        },
        // id của item được active
        activeID:{
            type:Number, 
        }
    }
}
</script>

<style>
@import url('../styles/base/context-menu.css');
</style>
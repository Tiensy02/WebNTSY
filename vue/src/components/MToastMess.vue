<template>
    <div v-if="isAppear" class="toast-mess-wrap">
        <div class="icon icon-success"></div>
        <div class="toast-mess-content">{{ contentMess }}</div>
    </div>
</template>

<script>
export default {
    name: "toast-mess",
    data() {
        return{
            
        }
    },
    watch:{
        isAppear(newValue) {
            setTimeout(()=>{
           this.$emit("hiddenToastMess")
        },4000)
        }     
    },
    props:{
        contentMess:{
            type: String,
            default:"content Message",
        },
        isAppear:{
            type:Boolean,
            default:false
        }
    }
}
</script>

<style>
@import url('../styles/base/toast-mess.css');
</style>
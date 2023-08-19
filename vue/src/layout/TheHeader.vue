<template>
    <div class="header">
        <div class="header__left">
            <div class="icon icon-grid"></div>
            <div class="header__left__logo">
                <div class="l-icon icon-logo icon--left"></div>
                <div class="header-title">MISA CeGov</div>
            </div>
        </div>
        <div class="header__right">
           <div class="icon noti-icon icon-circle"></div>
           <div class="icon guide-icon icon-circle"></div>
           <div class="icon book-icon icon-circle"></div>
           <div class="icon setting-icon icon-circle"></div>
           <div class="avatar-wrap">
               <div class="avatar"></div>
           </div>
        </div>
    </div>
</template>

<script>
export default {
    name: "the-header",
    data() {
        return {

        }
    }
}
</script>

<style>
@import url('../styles/layout/header.css');
</style>
<template>
  <div id="app">
    <the-header></the-header>
    <the-main></the-main>
    <the-sidebar></the-sidebar>
  </div>
</template>

<script>
import TheHeader from './layout/TheHeader.vue';
import TheSidebar from './layout/TheSidebar.vue';
import TheMain from './layout/TheMain.vue';
export default {
  name: 'app',
  data () {
    return {

    }
  },
  components:{
    TheMain,TheHeader,TheSidebar
  }
}
</script>

<style>
@import url('./styles/main.css');
:root {
    --error-color:#ef5350;
    --primary-color: #2979ff;
    --border-color: #E0E1E4;
    --disable-color:#f4f5f8;
    --secondary-color:#e0ebff;
    --primary-hover-color:#1a73e8;
    --secondary-hover-color:#eef4ff;
    --delete-color:#fef1f1;
    --danger-color:#e54340;
}

</style>

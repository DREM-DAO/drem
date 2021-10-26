<template>
  <div class="d-flex flex-column h-100">
    <div class="version">
      <a href="https://docs.globdrem.com" target="_blank">
        You are using TestNet version /////// You are using TestNet version
        /////// You are using TestNet version /////// You are using TestNet
        version /////// You are using TestNet version /////// You are using
        TestNet version /////// You are using TestNet version /////// You are
        using TestNet version /////// You are using TestNet version /////// You
        are using TestNet version /////// You are using TestNet version ///////
        You are using TestNet version /////// You are using TestNet version
        /////// You are using TestNet version /////// You are using TestNet
        version /////// You are using TestNet version /////// You are using
        TestNet version /////// You are using TestNet version /////// You are
        using TestNet version /////// You are using TestNet version /////// You
        are using TestNet version /////// You are using TestNet version ///////
        You are using TestNet version /////// You are using TestNet version
        /////// You are using TestNet version /////// You are using TestNet
        version /////// You are using TestNet version
      </a>
    </div>
    <slot name="header">
      <Navbar />
    </slot>
    <Toast />
    <div class="container-fluid d-flex flex-column flex-grow-1">
      <slot></slot>
    </div>
    <slot name="header">
      <Footer />
    </slot>
  </div>
</template>

<script>
import Toast from "primevue/toast";
import Navbar from "../components/Navbar.vue";
import Footer from "../components/Footer.vue";
import { mapActions } from "vuex";
export default {
  components: {
    Navbar,
    Footer,
    Toast,
  },
  created() {
    this.setVM({ _vm: this });
  },
  mounted() {
    this.setVM({ _vm: this });
  },
  methods: {
    ...mapActions({
      setVM: "toast/setVM",
    }),
  },
};
</script>
<style scoped>
.version {
  min-height: 16px;
  line-height: 12px;
  background: orange;
  color: white;
  margin: 0;
  width: 100%;
  overflow: hidden;
}
.version a {
  margin: 0;
  padding: 1px 10px;
  font-size: 10px;
  line-height: 10px;
  background: orange;
  color: white;
  white-space: nowrap;
  text-decoration: none;
  max-width: 100%;
}
</style>
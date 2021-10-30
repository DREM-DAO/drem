<template>
  <nav class="navbar navbar-expand-lg navbar-light bg-light">
    <div class="container-fluid">
      <a class="navbar-brand" href="/" v-if="$store.state.config.LOGO"
        ><img
          :src="$store.state.config.LOGO"
          height="70"
          :alt="$t('navbar.logo')"
        />
      </a>
      <a class="navbar-brand" href="/" v-else>{{ $t("navbar.logo") }}</a>

      <button
        class="navbar-toggler"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarSupportedContent"
        aria-controls="navbarSupportedContent"
        aria-expanded="false"
        :aria-label="$t('navbar.toggle_nav')"
      >
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav me-auto mb-2 mb-lg-0">
          <li class="nav-item active">
            <v-link class="nav-link" href="/map">Home</v-link>
          </li>
          <li class="nav-item active" v-if="$store.state.wallet.isOpen">
            <v-link class="nav-link" href="/new-account">{{
              $t("navbar.new_account")
            }}</v-link>
          </li>
          <li class="nav-item active">
            <v-link class="nav-link" href="/faq">{{ $t("navbar.faq") }}</v-link>
          </li>
          <li class="nav-item active">
            <v-link class="nav-link" href="/privacy-policy">{{
              $t("navbar.privacy_policy")
            }}</v-link>
          </li>
          <li class="nav-item active">
            <a class="nav-link" href="https://docs.globdrem.com">Docs</a>
          </li>
          <li class="nav-item dropdown">
            <!-- v-if="authData && authData.isAdmin" -->
            <a
              class="nav-link dropdown-toggle"
              href="#"
              id="adminDropdown"
              role="button"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            >
              Admin
            </a>
            <ul class="dropdown-menu" aria-labelledby="adminDropdown">
              <li>
                <v-link class="nav-link" href="/admin/value-sets"
                  >Value Sets</v-link
                >
              </li>
              <li>
                <v-link class="nav-link" href="/admin/projects"
                  >Projects</v-link
                >
              </li>
            </ul>
          </li>
        </ul>
        <ul class="navbar-nav ms-auto mb-2 mb-lg-0">
          <li class="nav-item active" v-if="!$store.state.wallet.isOpen">
            <v-link class="nav-link" href="/accounts">Open wallet</v-link>
          </li>
          <li class="nav-item active" v-if="$store.state.wallet.isOpen">
            <v-link class="nav-link" href="/accounts"
              >{{ $store.state.wallet.lastActiveAccountName
              }}<span v-if="this.$store.state.wallet.authTx"
                >&nbsp;<i class="pi pi-lock"></i></span
              ><span v-else>&nbsp;<i class="pi pi-unlock"></i></span>
            </v-link>
          </li>
          <Dropdown
            v-if="!$store.state.wallet.isOpen"
            v-model="$i18n.locale"
            :options="$store.state.config.languages"
            @change="languageUpdated"
            style="min-width: 100px"
          >
            <template #value="slotProps">
              <div v-if="slotProps.value" class="border-dark">
                <img
                  :alt="slotProps.value"
                  class="border-dark"
                  :src="'/flags/3x2/' + slotProps.value + '.svg'"
                  height="15"
                />
                <span class="m-1">{{ slotProps.value }}</span>
              </div>
              <span v-else>
                {{ slotProps.placeholder }}
              </span>
            </template>
            <template #option="slotProps">
              <div class="border-dark">
                <img
                  :alt="slotProps.option"
                  :src="'/flags/3x2/' + slotProps.option + '.svg'"
                  height="15"
                />
                <span class="m-1">{{ slotProps.option }}</span>
              </div>
            </template>
          </Dropdown>
          <li class="nav-item active" v-if="$store.state.wallet.isOpen">
            <v-link class="nav-link" href="/settings">{{
              $t("navbar.settings")
            }}</v-link>
          </li>
          <li class="nav-item active" v-if="$store.state.wallet.isOpen">
            <v-link class="nav-link" href="/" @click="logoutClick">{{
              $t("navbar.logout")
            }}</v-link>
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>
<script>
import { mapActions } from "vuex";
import VLink from "../components/VLink.vue";

export default {
  components: {
    VLink,
  },
  data() {
    return {};
  },
  computed: {
    token() {
      return this.$store.state.wallet.authTx;
    },
    authData() {
      return this.$store.state.wallet.me;
    },
  },
  watch: {
    async authData() {
      console.log("authData watch", this.authData);
    },
    async token() {},
  },
  methods: {
    ...mapActions({
      logout: "wallet/logout",
      axiosGet: "axios/get",
    }),
    logoutClick() {
      this.logout();
    },
    languageUpdated() {
      localStorage.setItem("lang", this.$i18n.locale);
    },
  },
};
</script>

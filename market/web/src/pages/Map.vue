<template>
  <PublicLayout>
    <l-map
      style="min-height: 80vh"
      :zoom="zoom"
      :min-zoom="minZoom"
      :max-zoom="maxZoom"
      :center="center"
    >
      <LTileLayer :url="url" :attribution="attribution" :options="options" />
      <LLayerGroup v-if="places">
        <LMarker
          v-for="place in places"
          :key="place.id"
          :lat-lng="getLatLng(place)"
        >
          <l-icon :icon-url="getIcon(place)" />
          <LPopup :options="{ autoClose: true, closeOnClick: false }">
            <h4 class="md-auto" style="text-align: center; min-width: 150px">
              {{ place.name }}
            </h4>
          </LPopup>
        </LMarker>
      </LLayerGroup>
    </l-map>
  </PublicLayout>
</template>

<script>
// DON'T load Leaflet components here!
// Its CSS is needed though, if not imported elsewhere in your application.
import "leaflet/dist/leaflet.css";
import {
  LMap,
  LTileLayer,
  LPopup,
  LMarker,
  LLayerGroup,
} from "@vue-leaflet/vue-leaflet";

import PublicLayout from "../layouts/Public.vue";
export default {
  components: {
    PublicLayout,
    LMap,
    LTileLayer,
    LPopup,
    LMarker,
    LLayerGroup,
  },
  data() {
    return {
      places: [
        {
          id: "1",
          tradeable: true,
          asa: "11",
          lat: 50,
          lng: 20,
          name: "AA",
        },
        {
          id: "1",
          tradeable: false,
          asa: "11",
          lat: 47.369450301672266,
          lng: 8.539875999999893,
          name: "Trust Square",
        },
        {
          id: "1",
          tradeable: false,
          asa: "11",
          lat: 50,
          lng: -71,
          name: "Canada Wood",
        },
      ],
      mapIsReady: false,
      minZoom: 3,
      maxZoom: 18,
      center: [30, 1],
      url: "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
      attribution:
        '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors',
      options: {
        useCache: true,
        crossOrigin: true,
      },
    };
  },
  async beforeMount() {
    this.mapIsReady = true;
  },
  methods: {
    getLatLng(branch) {
      if (!branch) return { lat: 0, lng: 0 };
      return { lat: branch.lat, lng: branch.lng };
    },
    getIcon(place) {
      if (place.tradeable) {
        return "https://cdnsitestoragecovid.blob.core.windows.net/web/images/icons/map_icon_orange.png";
      } else if (place.asa) {
        return "https://cdnsitestoragecovid.blob.core.windows.net/web/images/icons/map_icon_gray_01.png";
      } else {
        return "https://cdnsitestoragecovid.blob.core.windows.net/web/images/icons/map_icon_blue.png";
      }
    },
  },
};
</script>
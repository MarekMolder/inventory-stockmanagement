<script setup>
import PendingActionsCard from "@/components/PendingActionsCard.vue";
import ProblematicProducts from "@/components/ProblematicProducts.vue";
import LowestStockProductsCard from "@/components/LowestStockProductsCard.vue";
import TopUsersByRemoveCard from "@/components/TopUsersByRemoveCard.vue";
import TotalInventoryWorthCard from "@/components/TotalInventoryWorthCard.vue";
import ProductQuickSearchCard from "@/components/ProductQuickSearchCard.vue";
import UserListCard from "@/components/UserListCard.vue";
</script>

<template>
  <div class="dashboard">
    <div class="row row-top">
      <PendingActionsCard class="card wide"/>
      <ProblematicProducts class="card"/>
      <LowestStockProductsCard class="card"/>
      <ProductQuickSearchCard class="card"/>
    </div>
    <div class="row row-bottom">
      <TotalInventoryWorthCard class="card"/>
      <TopUsersByRemoveCard class="card"/>
      <UserListCard class="card"/>
    </div>
  </div>
</template>

<style scoped>
.dashboard-grid {
  display: flex;
  flex-direction: column;
  gap: 2rem;
  padding: 2rem 4rem;
}

.dashboard {
  display: flex;
  flex-direction: column;
  gap: 3rem;
  padding: 3rem 4rem;
}

.row {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: 2rem;
}

.row-top {
  justify-content: space-between;
}

.row-bottom {
  justify-content: center;
}

.card {
  background-color: #1e1e1e;
  padding: 1.5rem;
  border-radius: 1rem;
  box-shadow: 0 6px 20px rgba(255, 255, 255, 0.05);
  display: flex;
  flex-direction: column;
  flex: 1 1 auto;
  min-width: 240px;
  max-width: 320px;
  transition: all 0.3s ease;
}

.card:hover {
  transform: translateY(-6px) scale(1.02);
  box-shadow: 0 10px 25px rgb(220, 198, 70);
}

.card.wide {
  max-width: 400px;
}

.card.full {
  flex: 1 1 100%;
}


</style>

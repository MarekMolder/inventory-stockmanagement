<script setup lang="ts">
import { ref, onMounted } from "vue";
import { RoleService } from "@/services/RoleService";
import { IdentityService } from "@/services/IdentityService";
import type { AppRole } from "@/domain/logic/AppRole";
import type { AssignRoleDto } from "@/types/AssignRoleDto";

const roleService = new RoleService();
const identityService = new IdentityService();

const users = ref<{ id: string; firstName: string; lastName: string }[]>([]);
const roles = ref<AppRole[]>([]);
const selectedUserId = ref("");
const selectedRoleId = ref("");
const message = ref("");
const error = ref("");

const assign = async () => {
  const dto: AssignRoleDto = {
    userId: selectedUserId.value,
    roleId: selectedRoleId.value
  };
  const res = await roleService.assignRoleToUser(dto);
  if (res.errors!.length) {
    error.value = res.errors![0];
    message.value = "";
  } else {
    message.value = res.data!;
    error.value = "";
  }
};

onMounted(async () => {
  users.value = await identityService.getAllUsers();
  roles.value = await roleService.getAllRoles();
});
</script>

<template>
  <main class="action-index-wrapper">
    <div class="action-index-box">
      <h1 class="title">{{ $t('Assign role to user') }}</h1>

      <select v-model="selectedUserId" class="status-filter">
        <option value="" disabled>Select user</option>
        <option v-for="u in users" :value="u.id" :key="u.id">
          {{ u.firstName }} {{ u.lastName }}
        </option>
      </select>

      <select v-model="selectedRoleId" class="status-filter">
        <option value="" disabled>{{ $t('Select role') }}</option>
        <option v-for="r in roles" :value="r.id" :key="r.id">{{ r.name }}</option>
      </select>

      <button class="create-link" @click="assign">{{ $t('Assign') }}</button>

      <p v-if="message" style="color: lightgreen">{{ message }}</p>
      <p v-if="error" style="color: red">{{ error }}</p>
    </div>
  </main>
</template>

<style scoped>
.action-index-wrapper {
  display: flex;
  justify-content: center;
  margin-top: 5vh;
  font-family: Arial, sans-serif;
  color: white;
}

.action-index-box {
  background-color: #1a1a1a;
  padding: 2rem;
  border-radius: 12px;
  width: 90%;
  max-width: 800px;
  box-shadow: 0 0 12px rgba(255, 165, 0, 0.4);
  border: 3px solid orange;
}

.title {
  text-align: center;
  font-size: 2rem;
  color: orange;
  margin-bottom: 1rem;
}

.status-filter {
  background-color: #2a2a2a;
  color: white;
  border: 2px solid orange;
  padding: 0.4rem 0.8rem;
  border-radius: 8px;
  font-size: 1rem;
  font-weight: 500;
  margin-bottom: 1rem;
  width: 100%;
}

.create-link {
  background-color: orange;
  padding: 0.5rem 1rem;
  color: white;
  text-decoration: none;
  border-radius: 6px;
  font-weight: bold;
  transition: background 0.3s ease;
  display: block;
  width: 100%;
  text-align: center;
}

.create-link:hover {
  background-color: #ffaa33;
}

.styled-table {
  width: 100%;
  border-collapse: collapse;
  background-color: #2a2a2a;
}

.styled-table th,
.styled-table td {
  border: 1px solid #444;
  padding: 0.75rem;
  text-align: left;
}

.styled-table thead {
  background-color: #ff8c00;
  color: white;
}
</style>

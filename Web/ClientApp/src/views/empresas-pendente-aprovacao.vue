<template>
  <div class="container-xl">
    <h1>Empresas pendentes aprovação</h1>

    <div>
      <el-table :data="companies" style="width: 100%" @row-click="openItem">
        <TableColumn prop="name" label="Nome da Instituição"></TableColumn>
        <TableColumn prop="city" label="Cidade"></TableColumn>
        <TableColumn prop="state" label="UF"></TableColumn>
      </el-table>
    </div>

     <b-modal
      ref="aprovar-empresa"
      no-close-on-esc
      no-close-on-backdrop
      ok-only
      ok-title="Aprovar"
      @ok="aprovar">
      <template v-slot:modal-title>Aprovar empresa</template>
      <form>
          <div class="form-group">
            <label for="company-name">Nome da instituição<small aria-hidden="true">*</small></label>
              <input
                type="text"
                id="company-name"
                v-model="selectedCompany.name"
                :class="['form-control']"
                :disabled="true">
          </div>
          <div class="form-group">
            <label for="company-address">Endereço<small aria-hidden="true">*</small></label>
              <input
                type="text"
                id="company-address"
                v-model="selectedCompany.address"
                :class="['form-control']"
                :disabled="true">
          </div>
          <div class="form-row">
            <div class="form-group col-sm">
              <label for="company-district">Bairro<small aria-hidden="true">*</small></label>
                <input
                  type="text"
                  id="company-district"
                  v-model="selectedCompany.district"
                  :class="['form-control']"
                  :disabled="true">
            </div>
            <div class="form-group col-sm">
              <label for="company-city">Cidade<small aria-hidden="true">*</small></label>
                <input
                  type="text"
                  id="company-city"
                  v-model="selectedCompany.city"
                  :class="['form-control']"
                  :disabled="true">
            </div>
          </div>
          <div class="form-row">
            <div class="form-group col-sm">
              <label for="company-state">Estado<small aria-hidden="true">*</small></label>
                <select
                  id="company-state"
                  v-model="selectedCompany.state"
                  :class="['custom-select']"
                  :disabled="true">
                  <option value="">Selecione</option>
                  <option v-for="(estado, index) in estados" :key="index">{{ estado }}</option>
                </select>
            </div>
            <div class="form-group col-sm">
              <label for="company-zipCode">CEP<small aria-hidden="true">*</small></label>
                <the-mask
                  type="text"
                  id="company-zipCode"
                  placeholder="99999-999"
                  mask="#####-###"
                  v-model="selectedCompany.zipCode"
                  :class="['form-control']"
                  :disabled="true" />

            </div>
          </div>
          <div class="form-row">
            <div class="form-group col-sm">
              <label for="company-phone">Telefone<small aria-hidden="true">*</small></label>
                <the-mask
                  type="tel"
                  id="company-phone"
                  placeholder="(99) 99999-9999"
                  v-model="selectedCompany.phone"
                  :class="['form-control']"
                  :mask="['(##) ####-####', '(##) #####-####']"
                  :disabled="true" />
            </div>
            <div class="form-group col-sm">
              <label for="company-contactPerson">Nome para contato<small aria-hidden="true">*</small></label>
                <input
                  type="text"
                  id="company-contactPerson"
                  v-model="selectedCompany.contactPerson"
                  :class="['form-control']"
                  :disabled="true">
            </div>
          </div>
          <div class="form-group">
            <label for="quantidade-vouchers">Quantidade de Vouchers<small aria-hidden="true">*</small></label>
              <input
                type="number"
                id="quantidade-vouchers"
                v-model="quantidade"
                :class="['form-control']">
          </div>
        
        </form>
    </b-modal>
  </div>
</template>

<script lang="ts" src="./empresas-pendente-aprovacao.ts"></script>

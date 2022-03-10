﻿using AntDesign;
using Blazorise;
using Dignite.SiteBuilding.Localization;
using Dignite.SiteBuilding.Pages;
using Dignite.SiteBuilding.Permissions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.EntityActions;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;

namespace Dignite.SiteBuilding.Admin.Blazor.Pages.SiteBuilding.Admin.Pages
{
    public partial class Pages
    {       
        private Guid? ParentId { get; set; }

        protected PageToolbar Toolbar { get; } = new();

        protected List<TableColumn> PagesTableColumns => TableColumns.Get<Pages>();


        public Pages()
        {
            ObjectMapperContext = typeof(SiteBuildingAdminBlazorModule);
            LocalizationResource = typeof(SiteBuildingResource);

            CreatePolicyName = SiteBuildingPermissions.Page.Create;
            UpdatePolicyName = SiteBuildingPermissions.Page.Update;
            DeletePolicyName = SiteBuildingPermissions.Page.Delete;
        }


        protected override ValueTask SetEntityActionsAsync()
        {
            EntityActions
                .Get<Pages>()
                .AddRange(new EntityAction[]
                {
                    new EntityAction
                    {
                        Text = L["Edit"],
                        Visible = (data) => HasUpdatePermission,
                        Clicked = async (data) => { await OpenEditModalAsync(data.As<PageDto>()); }
                    },
                    new EntityAction
                    {
                        Text = L["Delete"],
                        Visible = (data) => HasDeletePermission,
                        Clicked = async (data) => await DeleteEntityAsync(data.As<PageDto>()),
                        ConfirmationMessage = (data) => GetDeleteConfirmationMessage(data.As<PageDto>())
                    }
                });

            return base.SetEntityActionsAsync();
        }

        protected override ValueTask SetTableColumnsAsync()
        {
            PagesTableColumns
                .AddRange(new TableColumn[]
                {
                    new TableColumn
                    {
                        Title = L["Actions"],
                        Actions = EntityActions.Get<Pages>()
                    },
                    new TableColumn
                    {
                        Title = L["Title"],
                        Data = nameof(PageDto.Title)
                    },
                    new TableColumn
                    {
                        Title = L["CreationTime"],
                        Data = nameof(PageDto.CreationTime),
                        DisplayFormat="{0:yyyy-MM-dd}"
                  },
                    new TableColumn
                    {
                        Title = L["LastModificationTime"],
                        Data = nameof(PageDto.LastModificationTime),
                        DisplayFormat="{0:yyyy-MM-dd}"
                    }
                });

            return base.SetTableColumnsAsync();
        }


        protected override string GetDeleteConfirmationMessage(PageDto entity)
        {
            return string.Format(L["RoleDeletionConfirmationMessage"], entity.Name);
        }

        protected override ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["NewPage"],
                OpenCreateModalAsync,
                IconName.Add,
                requiredPolicyName: CreatePolicyName);

            return base.SetToolbarItemsAsync();
        }

        protected override Task OnCreatingEntityAsync()
        {
            NewEntity.ParentId = ParentId;
            return base.OnCreatingEntityAsync();
        }

        protected override Task UpdateGetListInputAsync()
        {
            GetListInput.ParentId = ParentId;
            base.UpdateGetListInputAsync();

            return Task.CompletedTask;
        }

        async Task OnTreeNodeClick(TreeEventArgs<PageDto> e)
        {
            ParentId = e.Node.DataItem.Id;
            await base.GetEntitiesAsync();
        }
    }
}
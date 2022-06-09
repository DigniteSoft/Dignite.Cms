using AntDesign;
using Blazorise;
using Dignite.Cms.Localization;
using Dignite.Cms.Pages;
using Dignite.Cms.Permissions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.EntityActions;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;

namespace Dignite.Cms.Admin.Blazor.Pages.Cms.Admin.Pages
{
    public partial class Pages
    {       
        private Guid? ParentId { get; set; }

        protected PageToolbar Toolbar { get; } = new();

        protected List<TableColumn> PagesTableColumns => TableColumns.Get<Pages>();


        public Pages()
        {
            ObjectMapperContext = typeof(CmsAdminBlazorModule);
            LocalizationResource = typeof(CmsResource);

            CreatePolicyName = CmsPermissions.Page.Create;
            UpdatePolicyName = CmsPermissions.Page.Update;
            DeletePolicyName = CmsPermissions.Page.Delete;
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
                        Title = L["Title"],
                        Data = nameof(PageDto.Title)
                    },
                    new TableColumn
                    {
                        Title = L["Path"],
                        Data = nameof(PageDto.Path)
                    },
                    new TableColumn
                    {
                        Title = L["IsActive"],
                        Data = nameof(PageDto.IsActive)
                    },
                    new TableColumn
                    {
                        Title = L["Keywords"],
                        Data = nameof(PageDto.Keywords),
                        ValueConverter=(val)=>val.As<PageDto>().Keywords!=null?val.As<PageDto>().Keywords.JoinAsString(","):""
                    },
                    new TableColumn
                    {
                        Title = L["Description"],
                        Data = nameof(PageDto.Description)
                    },
                    new TableColumn
                    {
                        Title = L["TemplateFile"],
                        Data = nameof(PageDto.TemplateFile)
                    },
                    new TableColumn
                    {
                        Title = L["CreationTime"],
                        Data = nameof(PageDto.CreationTime)
                    },
                    new TableColumn
                    {
                        Title = L["Actions"],
                        Actions = EntityActions.Get<Pages>()
                    },
                });

            return base.SetTableColumnsAsync();
        }


        protected override string GetDeleteConfirmationMessage(PageDto entity)
        {
            return string.Format(L["PageDeletionConfirmationMessage"], entity.Name);
        }

        protected override ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["New"],
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

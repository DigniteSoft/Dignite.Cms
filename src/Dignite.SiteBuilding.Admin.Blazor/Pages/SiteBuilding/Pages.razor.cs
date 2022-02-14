using AntDesign.TableModels;
using Blazorise;
using Dignite.SiteBuilding.Localization;
using Dignite.SiteBuilding.Pages;
using Dignite.SiteBuilding.Permissions;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.EntityActions;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;

namespace Dignite.SiteBuilding.Admin.Blazor.Pages.SiteBuilding
{
    public partial class Pages
    {
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

        protected virtual async Task OnTableReadAsync(QueryModel<PageDto> e)
        {
            CurrentSorting = e.SortModel
<<<<<<< Updated upstream
                .OrderByDescending(c => c.Priority)
=======
                .Where(s=>!s.Sort.IsNullOrEmpty())
                .OrderByDescending(c=>c.Priority)
>>>>>>> Stashed changes
                .Select(c => c.FieldName + (c.Sort == "ascend" ? " ASC" : " DESC"))
                .JoinAsString(",");
            CurrentPage = e.PageIndex;

            await GetEntitiesAsync();

            await InvokeAsync(StateHasChanged);
        }

        protected async Task CreateEntityAsync1()
        {
            Console.WriteLine("Create1");
            try
            {
                var validate = true;
                if (CreateValidationsRef != null)
                {
                    validate = await CreateValidationsRef.ValidateAll();
                }
                if (validate)
                {
                    await OnCreatingEntityAsync();

                    await CheckCreatePolicyAsync();
                    var createInput = MapToCreateInput(NewEntity);
                    await AppService.CreateAsync(createInput);

                    await OnCreatedEntityAsync();
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }
    }
}

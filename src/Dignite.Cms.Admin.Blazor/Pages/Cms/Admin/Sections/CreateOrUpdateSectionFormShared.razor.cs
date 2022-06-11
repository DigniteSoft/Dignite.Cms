using Blazorise;
using Dignite.Abp.FieldCustomizing.Blazor;
using Dignite.Cms.Admin.Sections;
using Dignite.Cms.Localization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Web;

namespace Dignite.Cms.Admin.Blazor.Pages.Cms.Admin.Sections
{
    public partial class CreateOrUpdateSectionFormShared
    {
        [Inject] private IFieldControlComponentSelector fieldControlComponentSelector { get; set; }
        [Inject] private IFieldControlConfigurationComponentSelector fieldControlConfigurationComponentSelector { get; set; }

        [Parameter] public SectionCreateOrUpdateDtoBase Data { get; set; }

        //
        [Parameter] public IReadOnlyList<FieldControlProviderDto> AllFieldControlProviders { get; set; }

        [Parameter] public AbpBlazorMessageLocalizerHelper<CmsResource> LH { get; set; }
        [Parameter] public IStringLocalizer L { get; set; }
        [Parameter] public Validations SectionValidationsRef { get; set; }


        //选中字段的控件配置组件
        private Type? filedControlConfigurationSelectedType;

        //选中字段控件配置项组件的参数
        Dictionary<string, object> filedControlConfigurationSelectedParameters = new();



        //添加新字段
        //现在是点击添加，后期改为拖拽方式
        private async Task AddFieldAsync(MouseEventArgs e, string fieldControlName)
        {
            var validate = true;
            if (SectionValidationsRef != null)
            {
                validate = await SectionValidationsRef.ValidateAll();
            }
            if (validate)
            {
                var name = shortid.ShortId.Generate();
                var configuration = new Abp.FieldCustomizing.FieldControls.FieldControlConfigurationDictionary();
                var fieldContrlProvider = AllFieldControlProviders.Single(fcp => fcp.Name == fieldControlName);
                Data.FieldDefinitions.Add(new FieldDefinitionEditDto()
                {
                    Id = Guid.NewGuid(),
                    FieldControlProviderName = fieldContrlProvider.Name,
                    Name = name,
                    DisplayName = fieldContrlProvider.DisplayName,
                    Configuration = configuration
                });

                await SelectFieldControlAsync(e, name);
            }
        }

        private async Task SelectFieldControlAsync(MouseEventArgs e, string fieldDefinitionName)
        {
            var validate = true;
            if (SectionValidationsRef != null)
            {
                validate = await SectionValidationsRef.ValidateAll();
            }
            if (validate)
            {

                var fd = Data.FieldDefinitions.Single(df => df.Name == fieldDefinitionName);
                var component = fieldControlConfigurationComponentSelector.Get(fd.FieldControlProviderName);

                filedControlConfigurationSelectedParameters = new Dictionary<string, object>();
                filedControlConfigurationSelectedParameters.Add("Definition", fd);
                filedControlConfigurationSelectedType = component.GetType();
            }
        }

    }
}

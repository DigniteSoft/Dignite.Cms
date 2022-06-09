using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Microsoft.AspNetCore.Authorization;
using Dignite.Cms.Admin.Entries;
using Dignite.Abp.FileManagement;
using Dignite.Cms.Blobs;
using Volo.Abp.BlobStoring;
using Dignite.Cms.Admin.Blobs;
using Dignite.Abp.BlobStoring;
using Dignite.Cms.Permissions;

namespace Dignite.Cms.Admin
{
    [DependsOn(
        typeof(CmsDomainModule),
        typeof(CmsAdminApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(FileManagementApplicationModule)
        )]
    public class CmsAdminApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<CmsAdminApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<CmsAdminApplicationModule>(validate: true);
            });

            Configure<AuthorizationOptions>(options =>
            {
                options.AddPolicy("DigniteCmsCreatePolicy", policy => policy.Requirements.Add(CommonOperations.Create));
                options.AddPolicy("DigniteCmsUpdatePolicy", policy => policy.Requirements.Add(CommonOperations.Update));
                options.AddPolicy("DigniteCmsDeletePolicy", policy => policy.Requirements.Add(CommonOperations.Delete));
            });

            context.Services.AddSingleton<Microsoft.AspNetCore.Authorization.IAuthorizationHandler, EntryAuthorizationHandler>();


            Configure<Dignite.Abp.FileManagement.FileOptions>(options =>
                options.EntityTypes.AddRange(new FileEntityTypeDefinition[]{
                    new FileEntityTypeDefinition(EntityTypeConsts.Entry)
                })
            );


            //
            Configure((AbpBlobStoringOptions options) =>
            {
                options.Containers.Configure<FileEditFieldFileBlobContainer>(container =>
                {
                    container.SetNameGenerator<YearMonthBlobNameGenerator>();
                    container.SetAuthorizationHandler<AuthorizationHandler>(authorization =>
                    {
                        authorization.SavingPolicy = CmsPermissions.Entry.Create;
                        authorization.DeletingPolicy = CmsPermissions.Entry.Delete;
                    });
                    
                    container.AddFileTypeCheckHandler(fileTypeCheck =>
                    {
                        fileTypeCheck.AllowedFileTypeNames = new string[] { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".jpg", ".jpeg", ".png", ".gif", ".png", ".mp4", ".zip", ".rar", ".7z", ".txt" };
                    });                    

                    container.AddBlobSizeLimitHandler(fileSize =>
                    {
                        fileSize.MaximumBlobSize = 10;
                    });
                    container.AddImageResizeHandler(imageResize =>
                    {
                        imageResize.ImageWidth = 1500; //预设图片的宽度值；如果上传的图片宽度大于预设值，采用等比例缩小；
                        imageResize.ImageHeight = 2000; //预设图片的高度值；如果上传的图片高度大于预设值，采用等比例缩小；
                    });
                });
                options.Containers.Configure<RichTextEditorPicBlobContainer>(container =>
                {
                    container.SetNameGenerator<YearMonthBlobNameGenerator>();
                    container.SetAuthorizationHandler<AuthorizationHandler>(authorization =>
                    {
                        authorization.SavingPolicy = CmsPermissions.Entry.Create;
                        authorization.DeletingPolicy = CmsPermissions.Entry.Delete;
                    });

                    container.AddFileTypeCheckHandler(fileTypeCheck =>
                    {
                        fileTypeCheck.AllowedFileTypeNames = new string[] { ".jpeg", ".jpg", ".png", ".gif" };
                    });

                    container.AddBlobSizeLimitHandler(fileSize =>
                    {
                        fileSize.MaximumBlobSize = 10;
                    });

                    container.AddImageResizeHandler(imageResize =>
                    {
                        imageResize.ImageWidth = 800; //预设图片的宽度值；如果上传的图片宽度大于预设值，采用等比例缩小；
                        imageResize.ImageHeight = 1200; //预设图片的高度值；如果上传的图片高度大于预设值，采用等比例缩小；
                    });
                });
            });
        }
    }
}

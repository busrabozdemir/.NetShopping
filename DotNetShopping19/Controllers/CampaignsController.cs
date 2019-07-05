using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DotNetShopping.Models;

namespace DotNetShopping.Controllers
{
    public class CampaignsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Campaigns
        public async Task<ActionResult> Index()
        {
            return View(await db.Campaigns.ToListAsync());
        }

        // GET: Campaigns/Details/5
        public async Task<ActionResult> Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campaign campaign = await db.Campaigns.FindAsync(id);
            if (campaign == null)
            {
                return HttpNotFound();
            }
            return View(campaign);
        }

        // GET: Campaigns/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Campaigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CampaignId,Name,Description,Code,DiscountPercent,RequiredAmount" +
            "ProductCampaign,VariantCampaign,CodeCampaign,Enabled,OneTimeUse,FreeShipping,StartDate,EndDate")] CampaignEditModel campaignEdit)
        {
            if (ModelState.IsValid)
            {
                var campaign = new Campaign();
                campaign.Name = campaignEdit.Name;
                campaign.Description = campaignEdit.Description;
                campaign.Code = campaignEdit.Code;
                campaign.DiscountPercent = campaignEdit.DiscountPercent;
                campaign.RequiredAmount = campaignEdit.RequiredAmount;
                campaign.ProductCampaign = campaignEdit.ProductCampaign;
                campaign.VariantCampaign = campaignEdit.VariantCampaign;
                campaign.CodeCampaign = campaignEdit.CodeCampaign;
                campaign.Enabled = campaignEdit.Enabled;
                campaign.OneTimeUse = campaignEdit.OneTimeUse;
                campaign.FreeShipping = campaignEdit.FreeShipping;
                campaign.StartDate = campaignEdit.StartDate;
                campaign.EndDate = campaignEdit.EndDate;

                db.Campaigns.Add(campaign);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(campaignEdit);
        }

        // GET: Campaigns/Edit/5
        public async Task<ActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campaign campaign = await db.Campaigns.FindAsync(id);
            if (campaign == null)
            {
                return HttpNotFound();
            }
            var campaignEdit = new CampaignEditModel();
            campaignEdit.CampaignId = campaign.CampaignId;
            campaignEdit.Name = campaign.Name;
            campaignEdit.Code = campaign.Code;
            campaignEdit.CodeCampaign = campaign.CodeCampaign;
            campaignEdit.Description = campaign.Description;
            campaignEdit.DiscountPercent = campaign.DiscountPercent;
            campaignEdit.RequiredAmount = campaign.RequiredAmount;
            campaignEdit.Enabled = campaign.Enabled;
            campaignEdit.EndDate = campaign.EndDate;
            campaignEdit.FreeShipping = campaign.FreeShipping;
            campaignEdit.OneTimeUse = campaign.OneTimeUse;
            campaignEdit.ProductCampaign = campaign.ProductCampaign;
            campaignEdit.StartDate = campaign.StartDate;
            campaignEdit.VariantCampaign = campaign.VariantCampaign;
            return View(campaignEdit);
        }

        // POST: Campaigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CampaignId,Name,Description,Code,DiscountPercent,RequiredAmount,ProductCampaign,VariantCampaign,CodeCampaign,Enabled,OneTimeUse,FreeShipping,StartDate,EndDate")] CampaignEditModel campaignEdit)
        {
            if (ModelState.IsValid)
            {
                var campaign = db.Campaigns.Find(campaignEdit.CampaignId);
                campaign.Name = campaignEdit.Name;
                campaign.Description = campaignEdit.Description;
                campaign.Code = campaignEdit.Code;
                campaign.DiscountPercent = campaignEdit.DiscountPercent;
                campaign.RequiredAmount = campaignEdit.RequiredAmount;
                campaign.ProductCampaign = campaignEdit.ProductCampaign;
                campaign.VariantCampaign = campaignEdit.VariantCampaign;
                campaign.CodeCampaign = campaignEdit.CodeCampaign;
                campaign.Enabled = campaignEdit.Enabled;
                campaign.OneTimeUse = campaignEdit.OneTimeUse;
                campaign.FreeShipping = campaignEdit.FreeShipping;
                campaign.StartDate = campaignEdit.StartDate;
                campaign.EndDate = campaignEdit.EndDate;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(campaignEdit);
        }

        // GET: Campaigns/Delete/5
        public async Task<ActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campaign campaign = await db.Campaigns.FindAsync(id);
            if (campaign == null)
            {
                return HttpNotFound();
            }
            return View(campaign);
        }

        // POST: Campaigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(short id)
        {
            Campaign campaign = await db.Campaigns.FindAsync(id);
            db.Campaigns.Remove(campaign);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

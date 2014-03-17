using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusConductor.UI.ViewModels.Home;
using System.ServiceModel.Syndication;

namespace BusConductor.UI.ViewModelMappers.Home
{
    public static class IndexViewModelMapper
    {
        public static IndexViewModel Map(SyndicationFeed blogFeed)
        {
            var viewModel = new IndexViewModel();
            viewModel.BlogPosts = new List<IndexViewModelBlogPost>();


            foreach (SyndicationItem blogPost in blogFeed.Items)
            {
                var indexViewModelBlogPost = new IndexViewModelBlogPost();
                indexViewModelBlogPost.Title = blogPost.Title.Text;//string.Format("<a href='{0}'>{1}</a>", blogPost.Links[0].Uri, );
                indexViewModelBlogPost.Url = string.Format("{0}", blogPost.Links[0].Uri);
                //indexViewModelBlogPost.Content = StreamToString(blogPost.Content.
                viewModel.BlogPosts.Add(indexViewModelBlogPost);


                //// album.links[0].URI points to this album page on spaces.live.com
                //// album.Summary (not shown) is an HTML block with thumbnails of the album pics
                //var cell.Text = string.Format("<a href='{0}'>{1}</a>", album.Links[0].Uri, album.Title.Text);
                //albumRSS = GetAlbumRSS(album);
                //r = XmlReader.Create(albumRSS);
                //photos = SyndicationFeed.Load(r);
                //r.Close();
                //foreach (SyndicationItem photo in photos.Items)
                //{
                //    // photo.Summary is an HTML block with a thumbnail of the pic
                //    cell.Text = string.Format("{0}", photo.Summary.Text);
                //}

            }


            return viewModel;
        }

    }
}
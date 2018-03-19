using Automation.PagesObjects;
using MongoDB.Bson;
using NUnit.Framework;
using Automation.PagesObjects.ExternalPagesobjects;
using System.Linq;

namespace Automation.TestsFolder
{
    [TestFixture]
    public class NavigationTests
    {
        [TestFixture]
        [Parallelizable]
        public class Fixtures : BaseUi
        {
            [Test]
            [Property("TestCaseId", "99")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Retry(1)]
            public void FixturesIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }


        [TestFixture]
        [Parallelizable]
        public class Transfers : BaseUi
        {
            [Test]
            [Property("TestCaseId", "105")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Retry(1)]
            public void TransfersIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Buzz : BaseUi
        {
            [Test]
            [Property("TestCaseId", "107")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Retry(1)]
            public void BuzzIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class UEFA : BaseUi
        {
            [Test]
            [Property("TestCaseId", "108")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Retry(1)]
            public void UEFAIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class WorldNews : BaseUi
        {
            [Test]
            [Property("TestCaseId", "109")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Retry(1)]
            public void WorldNewsIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Lists : BaseUi
        {
            [Test]
            [Property("TestCaseId", "110")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Retry(1)]
            public void ListsIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Viral : BaseUi
        {
            [Test]
            [Property("TestCaseId", "111")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Retry(1)]
            public void ViralIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Conmebol : BaseUi
        {
            [Test]
            [Property("TestCaseId", "112")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Retry(1)]
            public void ConmebolIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }


        [TestFixture]
        [Parallelizable]
        public class Rumors : BaseUi
        {
            [Test]
            [Property("TestCaseId", "113")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Retry(1)]
            public void RumorsIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }


        [TestFixture]
        [Parallelizable]
        public class LocalCoverage : BaseUi
        {
            [Test]
            [Property("TestCaseId", "114")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Retry(1)]
            public void LocalCoverageIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Esports : BaseUi
        {
            [Test]
            [Property("TestCaseId", "115")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Retry(1)]
            public void EsportsIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }


        [TestFixture]
        [Parallelizable]
        public class Highlights : BaseUi
        {
            [Test]
            [Property("TestCaseId", "116")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Retry(1)]
            public void HighlightsIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Quizzes : BaseUi
        {
            [Test]
            [Property("TestCaseId", "117")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Retry(1)]
            public void QuizzesIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }


        [TestFixture]
        [Parallelizable]
        public class Tv : BaseUi
        {
            [Test]
            [Property("TestCaseId", "118")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Retry(1)]
            public void TvIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Music : BaseUi
        {
            [Test]
            [Property("TestCaseId", "119")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Retry(1)]
            public void MusicIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Gossip : BaseUi
        {
            [Test]
            [Property("TestCaseId", "120")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Retry(1)]
            public void GossipIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Video : BaseUi
        {
            [Test]
            [Property("TestCaseId", "121")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Retry(1)]
            public void VideoIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class ESL : BaseUi
        {
            [Test]
            [Property("TestCaseId", "122")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Retry(1)]
            public void ESLIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class DreamHack : BaseUi
        {
            [Test]
            [Property("TestCaseId", "123")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Retry(1)]
            public void DreamHackIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }


        [TestFixture]
        [Parallelizable]
        public class MDL : BaseUi
        {
            [Test]
            [Property("TestCaseId", "124")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Retry(1)]
            public void MDLIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Fnatic : BaseUi
        {
            [Test]
            [Property("TestCaseId", "125")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Retry(1)]
            public void FnaticIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }


        [TestFixture]
        [Parallelizable]
        public class iLeague : BaseUi
        {
            [Test]
            [Property("TestCaseId", "126")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Retry(1)]
            public void iLeagueIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }
    }
}